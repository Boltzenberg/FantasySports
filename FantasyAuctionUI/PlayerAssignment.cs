using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class PlayerAssignment : Form
    {
        private League league;
        private List<IPlayer> allPlayers;
        private IPlayer currentPlayer;
        private string yahooAuthToken = string.Empty;
        private string fileName = string.Empty;

        public PlayerAssignment()
        {
            InitializeComponent();
            this.LoadPlayers(Path.Combine("C:\\users\\jon_r\\onedrive\\documents", Constants.Files.League));
        }

        private void OnLoadLeague(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.DefaultExt = "json";
                dlg.Multiselect = false;
                dlg.Title = "Select League File";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.LoadPlayers(dlg.FileName);
                }
            }
        }

        private void OnCreateNewLeague(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.AddExtension = true;
                dlg.CheckFileExists = false;
                dlg.CheckPathExists = true;
                dlg.CreatePrompt = false;
                dlg.DefaultExt = "json";
                dlg.OverwritePrompt = true;
                dlg.Title = "Select New League Filename";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PromptFromList prompt = new PromptFromList("Select Fantasy League", Enum.GetNames(typeof(Leagues)));
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        Leagues fantasyLeague;
                        if (Enum.TryParse(prompt.SelectedItem, out fantasyLeague))
                        {
                            League league = League.Create(fantasyLeague);
                            league.Save(dlg.FileName);
                            this.LoadPlayers(dlg.FileName);
                        }
                    }
                }
            }
        }

        private void WriteUIToCurrentPlayer()
        {
            if (this.currentPlayer != null)
            {
                if (this.cbFantasyTeam.SelectedItem != null && !string.IsNullOrEmpty(this.cbFantasyTeam.SelectedItem.ToString()))
                {
                    this.currentPlayer.FantasyTeam = this.cbFantasyTeam.SelectedItem.ToString();
                }
                else if (string.IsNullOrEmpty(this.tbFantasyTeam.Text) || Array.Exists(this.league.Teams, x => x.Name == this.tbFantasyTeam.Text))
                {
                    this.currentPlayer.FantasyTeam = this.tbFantasyTeam.Text;
                }

                if (string.IsNullOrEmpty(this.currentPlayer.FantasyTeam) && 
                    (string.IsNullOrEmpty(this.tbAssumedFantasyTeam.Text) || Array.Exists(this.league.Teams, x => x.Name == this.tbAssumedFantasyTeam.Text)))
                {
                    this.currentPlayer.AssumedFantasyTeam = this.tbAssumedFantasyTeam.Text;
                }

                float price;
                if (float.TryParse(this.tbAuctionPrice.Text, out price))
                {
                    this.currentPlayer.AuctionPrice = price;
                }
                else if (string.IsNullOrEmpty(this.tbAuctionPrice.Text))
                {
                    this.currentPlayer.AuctionPrice = 0.0f;
                }
            }
        }

        private string GetPlayerAnalysisTable(IPlayer player, IEnumerable<IPlayer> players)
        {
            PercentilePlayerGroupAnalyzer analyzer = new PercentilePlayerGroupAnalyzer();
            LeagueConstants lc = LeagueConstants.For(this.league.FantasyLeague);
            IEnumerable<IStatExtractor> extractors = player is Batter ? lc.BattingSupportingStatExtractors.Union(lc.BattingScoringStatExtractors) : lc.PitchingSupportingStatExtractors.Union(lc.PitchingScoringStatExtractors);

            StringBuilder sb = new StringBuilder();
            sb.Append("<TABLE BORDER=1><TR><TD>Stat Name</TD><TD>Player's Stat Value</TD><TD>Max Value</TD><TD>Min Value</TD><TD>Graph</TD><TD>Player Percentile</TD></TR>");
            foreach (IStatExtractor extractor in extractors)
            {
                PlayerGroupAnalysis analysis = analyzer.Analyze(this.Text, extractor, players, player, p => p == player ? Brushes.Yellow : !string.IsNullOrEmpty(p.FantasyTeam) ? Brushes.Blue : Brushes.Violet);
                byte[] img;
                using (MemoryStream stm = new MemoryStream())
                {
                    analysis.Graph.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    img = stm.ToArray();
                }
                sb.AppendLine($"<TR><TD>{analysis.Stat}</TD><TD>{analysis.PlayerStatValue}</TD><TD>{analysis.MaxStatValue}</TD><TD>{analysis.MinStatValue}</TD><TD><img src=\"data:image/jpg;base64,{Convert.ToBase64String(img)}\"/></TD><TD>{analysis.PlayerPercentile}</TD></TR>");
            }
            sb.AppendLine("</TABLE>");
            return sb.ToString();
        }

        private void UpdateCurrentPlayer(IPlayer newCurrentPlayer)
        {
            this.WriteUIToCurrentPlayer();
            if (newCurrentPlayer != null)
            {
                this.tbAuctionPrice.Text = newCurrentPlayer.AuctionPrice.ToString();
                this.tbFantasyTeam.Text = newCurrentPlayer.FantasyTeam;
                this.cbFantasyTeam.SelectedItem = newCurrentPlayer.FantasyTeam;
                this.tbAssumedFantasyTeam.Text = newCurrentPlayer.AssumedFantasyTeam;

                StringBuilder sb = new StringBuilder();
                sb.Append("<HTML><BODY><H1>Player Info</H1>");
                sb.Append(newCurrentPlayer.GetHTML());
                sb.Append("<H1>Player analysis vs all players</H1>");
                sb.Append(this.GetPlayerAnalysisTable(newCurrentPlayer, this.league.AllPlayers));
                foreach (Position position in newCurrentPlayer.Positions)
                {
                    sb.AppendFormat("<H1>Player analysis vs all {0}</H1>", position);
                    sb.Append(this.GetPlayerAnalysisTable(newCurrentPlayer, this.league.AllPlayers.Where(p => p.Positions.Contains(position))));
                }
                sb.Append("</BODY></HTML>");
                this.wbOut.DocumentText = sb.ToString();
            }
            this.currentPlayer = newCurrentPlayer;
        }

        private void OnSelectPlayer(object sender, EventArgs e)
        {
            UpdateCurrentPlayer(this.lbPlayers.SelectedItem as IPlayer);
        }

        private void LoadPlayers(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            this.fileName = fileName;
            this.league = League.Load(fileName);
            this.allPlayers = new List<IPlayer>();
            this.allPlayers.AddRange(this.league.AllPlayers);
            this.allPlayers.Sort((x, y) => x.Name.CompareTo(y.Name));
            this.tbWordWheel.Text = string.Empty;
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.ToArray());
            this.currentPlayer = null;
            List<string> teams = new List<string>(this.league.Teams.Select(t => t.Name));
            teams.Sort();
            teams.Insert(0, string.Empty);
            this.cbFantasyTeam.Items.Clear();
            this.cbFantasyTeam.Items.AddRange(teams.ToArray());
        }

        private void OnSave(object sender, EventArgs e)
        {
            this.WriteUIToCurrentPlayer();
            this.league.Save(this.fileName);
        }

        private void OnReloadESPNData(object sender, EventArgs e)
        {
            this.league.UpdateESPNProjections();
        }

        private async void OnReloadYahooData(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.yahooAuthToken))
            {
                System.Diagnostics.Process.Start(YahooFantasySports.Services.AuthManager.GetAuthUrl().AbsoluteUri);
                Prompt prompt = new Prompt("Enter Auth Code:");
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    await YahooFantasySports.Services.AuthManager.Instance.InitializeAuthorizationAsync(prompt.Answer);
                    this.yahooAuthToken = prompt.Answer;
                }
            }

            if (!string.IsNullOrEmpty(this.yahooAuthToken))
            {
                await this.league.UpdateYahooData();
            }
        }

        private void OnLaunchStatCenter(object sender, EventArgs e)
        {
            new StatCenter(this.league.Clone(), p => p.FantasyTeam).Show();
        }

        private void OnLaunchRosterCenter(object sender, EventArgs e)
        {
            new RosterCenter(this.league.Clone()).Show();
        }

        private void OnLaunchAnalysisCenter(object sender, EventArgs e)
        {
            PromptFromList prompt = new PromptFromList("Select Team", this.league.Teams.Select(t => t.Name));
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                new AnalysisCenter(this.league.Clone(), this.league.Teams.First(t => t.Name == prompt.SelectedItem)).Show();
            }
        }

        private void OnLaunchSpeculationCenter(object sender, EventArgs e)
        {
            new StatCenter(this.league.Clone(), p => string.IsNullOrEmpty(p.FantasyTeam) ? p.AssumedFantasyTeam : p.FantasyTeam).Show();
        }

        private void OnLaunchTargetCenter(object sender, EventArgs e)
        {
            new TargetCenter(this.league.Clone()).Show();
        }

        private void OnLaunchSnakeCenter(object sender, EventArgs e)
        {
            PromptFromList prompt = new PromptFromList("Select Your Team", this.league.Teams.Select(t => t.Name));
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                new SnakeCenter(this.league, this.fileName, prompt.SelectedItem).Show();
            }
        }

        private void OnLaunchPlayerGroupAnalysisCenter(object sender, EventArgs e)
        {
            new PlayerGroupCenter("All players", LeagueConstants.For(this.league.FantasyLeague), this.league.AllPlayers).Show();
        }

        private void OnWordWheel(object sender, EventArgs e)
        {
            this.lbPlayers.BeginUpdate();
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.Where(x => x.Name.StartsWith(this.tbWordWheel.Text, StringComparison.OrdinalIgnoreCase)).ToArray());
            this.lbPlayers.EndUpdate();
        }

        private void OnGetTopFreeAgentSwaps(object sender, EventArgs e)
        {
            PromptFromList prompt = new PromptFromList("Select Team", this.league.Teams.Select(t => t.Name));
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string html = FantasyAlgorithms.RosterAnalysis.GetTopNFreeAgentPickups(this.league.Clone(), LeagueConstants.For(this.league.FantasyLeague).ScoringStatExtractors, prompt.SelectedItem);
                this.wbOut.DocumentText = html;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                // Focus on the Player textbox and select all
                this.tbWordWheel.Focus();
                this.tbWordWheel.SelectAll();
            }
        }
    }
}
