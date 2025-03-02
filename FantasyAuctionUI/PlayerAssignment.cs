using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using NirSiteLib;
using NirSiteLib.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private NirDriver driver = null;

        public PlayerAssignment()
        {
            InitializeComponent();
            this.LoadPlayers(Path.Combine(ConfigLib.Directories.LeagueFileDirectory, Constants.Files.League));
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

        private string GetPlayerAnalysisTable(IPlayer player, IEnumerable<IPlayer> players, string tableTitle)
        {
            PercentilePlayerGroupAnalyzer analyzer = new PercentilePlayerGroupAnalyzer();
            LeagueConstants lc = LeagueConstants.For(this.league.FantasyLeague);
            IEnumerable<IStatExtractor> extractors = player is Batter ? lc.BattingSupportingStatExtractors.Union(lc.BattingScoringStatExtractors) : lc.PitchingSupportingStatExtractors.Union(lc.PitchingScoringStatExtractors);

            StringBuilder sb = new StringBuilder();
            sb.Append("<TABLE BORDER=1><TR><TD>Stat Name</TD><TD>Player's Stat Value</TD><TD>Max Value</TD><TD>Min Value</TD><TD>Graph</TD><TD>Player Percentile</TD></TR>");
            int percentileSum = 0;
            int count = 0;
            foreach (IStatExtractor extractor in extractors)
            {
                using (PlayerGroupAnalysis analysis = analyzer.Analyze(this.Text, extractor, players, player, p => p == player ? Brushes.Yellow : !string.IsNullOrEmpty(p.FantasyTeam) ? Brushes.Blue : Brushes.Violet))
                {
                    byte[] img;
                    using (MemoryStream stm = new MemoryStream())
                    {
                        analysis.Graph.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img = stm.ToArray();
                    }
                    sb.AppendLine($"<TR><TD>{analysis.Stat}</TD><TD>{analysis.PlayerStatValue}</TD><TD>{analysis.MaxStatValue}</TD><TD>{analysis.MinStatValue}</TD><TD><img src=\"data:image/jpg;base64,{Convert.ToBase64String(img)}\"/></TD><TD>{analysis.PlayerPercentile}</TD></TR>");
                    percentileSum += analysis.PlayerPercentile;
                    count++;
                }
            }
            sb.AppendLine("</TABLE>");

            return string.Format("<H1>{0} (Average Percentile: {1})</H1>{2}", tableTitle, percentileSum / count, sb.ToString());
        }

        private string GetPlayerHTML(IPlayer newCurrentPlayer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><BODY><H1>Player Info</H1>");
            sb.Append(newCurrentPlayer.GetHTML());
            sb.Append(this.GetPlayerAnalysisTable(newCurrentPlayer, this.league.AllPlayers, "Player analysis vs all players"));
            foreach (Position position in newCurrentPlayer.Positions)
            {
                IEnumerable<IPlayer> positionPlayers = this.league.AllPlayers.Where(p => p.Positions.Contains(position));
                if (position == Position.P)
                {
                    positionPlayers = this.league.Pitchers.Where(p => p.IsSP == ((Pitcher)newCurrentPlayer).IsSP);
                }
                sb.Append(this.GetPlayerAnalysisTable(newCurrentPlayer, positionPlayers, string.Format("Player analysis vs all {0}", position)));
            }
            sb.Append("</BODY></HTML>");
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
                this.wbOut.DocumentText = this.GetPlayerHTML(newCurrentPlayer);
            }
            this.currentPlayer = newCurrentPlayer;
        }

        private void OnSelectPlayer(object sender, EventArgs e)
        {
            UpdateCurrentPlayer(this.lbPlayers.SelectedItem as IPlayer);
        }

        private void SetPlayerList(IEnumerable<IPlayer> players)
        {
            this.allPlayers = new List<IPlayer>();
            this.allPlayers.AddRange(players);
            this.allPlayers.Sort((x, y) => x.Name.CompareTo(y.Name));
            this.tbWordWheel.Text = string.Empty;
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.ToArray());
            this.currentPlayer = null;
        }

        private void LoadPlayers(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            this.fileName = fileName;
            this.league = League.Load(fileName);
            this.SetPlayerList(this.league.AllPlayers);
            if (this.league.Teams != null)
            {
                List<string> teams = new List<string>(this.league.Teams.Select(t => t.Name));
                teams.Sort();
                teams.Insert(0, string.Empty);
                this.cbFantasyTeam.Items.Clear();
                this.cbFantasyTeam.Items.AddRange(teams.ToArray());
            }
            this.Text = "Fantasy Auction - " + Path.GetFileNameWithoutExtension(this.fileName);
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
                List<YahooFantasySports.DataModel.Player> unmatchedYahooPlayers = await this.league.UpdateYahooData();
                List<IPlayer> espnPlayersWithoutYahooIds = new List<IPlayer>(this.league.AllPlayers.Where(p => string.IsNullOrEmpty(p.YahooId)));
                new PlayerMatcher(espnPlayersWithoutYahooIds, unmatchedYahooPlayers).Show();
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

        private void OnLaunchAllPlayersStatCenter(object sender, EventArgs e)
        {
            new AllPlayersStatCenter(this.league.Clone()).Show();
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

        private void OnLaunchLeagueSettings(object sender, EventArgs e)
        {
            new LeagueSettings(this.league).Show();
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

        private string SanitizeTeamName(string n)
        {
            StringBuilder s = new StringBuilder();
            foreach (char c in n)
            {
                if (Char.IsLetterOrDigit(c))
                {
                    s.Append(Char.ToLowerInvariant(c));
                }
            }

            return s.ToString();
        }

        private string SanitizePlayerName(string p)
        {
            return p.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ñ', 'n').Replace('ó', 'o').Replace('ú', 'u');
        }

        private bool MatchPlayer(IPlayer player1, RosteredPlayer player2)
        {
            if (!string.IsNullOrEmpty(player1.YahooId) &&
                player1.YahooId == player2.YahooId)
            {
                return true;
            }

            if (SanitizePlayerName(player1.Name) == SanitizePlayerName(player2.Name))
            {
                return true;
            }

            return false;
        }

        private void OnImportFromNirSite(object sender, EventArgs e)
        {
            if (this.driver == null)
            {
                this.driver = new NirDriver();
            }

            Dictionary<string, List<RosteredPlayer>> rosters = this.driver.GetTeamRosters();

            foreach (string teamName in rosters.Keys)
            {
                Team team = this.league.Teams.Where(t => SanitizeTeamName(t.Name) == SanitizeTeamName(teamName)).FirstOrDefault();
                if (team == null)
                {
                    // Fix this from Nir's site!
                    MessageBox.Show("Failed to find team " + teamName);
                    return;
                }
            }

            foreach (IPlayer player in this.league.AllPlayers)
            {
                player.AuctionPrice = 0;
                player.FantasyTeam = string.Empty;
            }

            List<ImportResult> errors = new List<ImportResult>();
            foreach (string teamName in rosters.Keys)
            {
                foreach (RosteredPlayer rosteredPlayer in rosters[teamName])
                {
                    List<IPlayer> allPlayers = new List<IPlayer>(this.league.AllPlayers.Where(p => MatchPlayer(p, rosteredPlayer)));
                    if (allPlayers.Count == 0)
                    {
                        errors.Add(new ImportResult() { Player = rosteredPlayer.Name, MLBTeam = rosteredPlayer.MLBTeamName, Positions = rosteredPlayer.Positions, FantasyTeam = teamName, Cost = rosteredPlayer.Price.ToString(), Error = "No player with that name in the league data!" });
                        continue;
                    }

                    if (allPlayers.Count > 1)
                    {
                        // Disambiguate by batter or pitcher
                        allPlayers = new List<IPlayer>(allPlayers.Where(p => p.IsBatter != rosteredPlayer.Positions.Contains("P")));
                        if (allPlayers.Count != 1)
                        {
                            errors.Add(new ImportResult() { Player = rosteredPlayer.Name, MLBTeam = rosteredPlayer.MLBTeamName, Positions = rosteredPlayer.Positions, FantasyTeam = teamName, Cost = rosteredPlayer.Price.ToString(), Error = "Too many players with that name in the league data!" });
                            continue;
                        }
                    }

                    IPlayer player = allPlayers[0];
                    if (player != null)
                    {
                        if (string.IsNullOrEmpty(player.FantasyTeam))
                        {
                            player.FantasyTeam = teamName;
                            player.AuctionPrice = rosteredPlayer.Price;

                            if (player.Name == "Shohei Ohtani")
                            {
                                IPlayer otherOhtani = this.league.AllPlayers.Where(p => p.Name == player.Name && p != player).FirstOrDefault();
                                if (otherOhtani != null)
                                {
                                    otherOhtani.FantasyTeam = teamName;
                                    otherOhtani.AuctionPrice = 0;
                                }
                            }
                        }
                        else if (player.FantasyTeam.ToLowerInvariant() != teamName.ToLowerInvariant() || player.AuctionPrice != rosteredPlayer.Price)
                        {
                            errors.Add(new ImportResult() { Player = rosteredPlayer.Name, MLBTeam = rosteredPlayer.MLBTeamName, Positions = rosteredPlayer.Positions, FantasyTeam = teamName, Cost = rosteredPlayer.Price.ToString(), Error = "Player data isn't right!" });
                        }
                    }
                    else
                    {
                        errors.Add(new ImportResult() { Player = rosteredPlayer.Name, MLBTeam = rosteredPlayer.MLBTeamName, Positions = rosteredPlayer.Positions, FantasyTeam = teamName, Cost = rosteredPlayer.Price.ToString(), Error = "No player with that name in the league data!" });
                    }
                }
            }

            if (errors.Count > 0)
            {
                new ImportResults(errors).Show();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.driver != null)
            {
                this.driver.Dispose();
                this.driver = null;
            }

            base.OnClosing(e);
        }

        private void OnFilterToAllPlayers(object sender, EventArgs e)
        {
            this.SetPlayerList(this.league.AllPlayers);
        }

        private bool MatchPlayer(IPlayer player1, BidItem player2)
        {
            if (!string.IsNullOrEmpty(player1.YahooId) &&
                player1.YahooId == player2.YahooId)
            {
                return true;
            }

            if (SanitizePlayerName(player1.Name) == SanitizePlayerName(player2.PlayerName))
            {
                return true;
            }

            return false;
        }

        private void FilterToAuctionEndingPlayers(bool byTomorrow)
        {
            List<IPlayer> players = new List<IPlayer>();
            if (this.driver == null)
            {
                this.driver = new NirDriver();
            }

            List<BidItem> bidItems = this.driver.GetPlayersUpForAuction();
            foreach (BidItem bidItem in bidItems)
            {
                if (byTomorrow && bidItem.TimeLeft.Ticks > TimeSpan.TicksPerDay)
                {
                    continue;
                }

                IPlayer player = this.league.AllPlayers.FirstOrDefault(p => MatchPlayer(p, bidItem));
                if (player != null)
                {
                    players.Add(player);
                }
                else
                {
                    MessageBox.Show("Failed to find player " + bidItem.PlayerName + " in the player list!", bidItem.PlayerName);
                }
            }

            this.SetPlayerList(players);
        }

        private void OnFilterToAuctionEndingPlayers(object sender, EventArgs e)
        {
            this.FilterToAuctionEndingPlayers(false);
        }

        private void OnFilterToAuctionEndingTomorrowPlayers(object sender, EventArgs e)
        {
            this.FilterToAuctionEndingPlayers(true);
        }

        private void OnExportWebUI(object sender, EventArgs e)
        {
            this.folderPicker.ShowNewFolderButton = true;
            if (this.folderPicker.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string folder = this.folderPicker.SelectedPath;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            StringBuilder sbIndex = new StringBuilder();
            sbIndex.AppendFormat("<HTML><HEAD><TITLE>Fantasy Baseball info for {0} generated at {1}</TITLE></HEAD><BODY>", this.league.FantasyLeague, DateTime.Now.ToString("yyyy-MM-dd"));
            sbIndex.AppendLine("<H1>Player List</H1><OL>");

            foreach (IPlayer player in this.league.AllPlayers)
            {
                sbIndex.AppendFormat("<LI><a href=\"{0}.html\">{1}</a></LI>", player.ESPNId, player.Name);
                File.WriteAllText(Path.Combine(folder, player.ESPNId + ".html"), this.GetPlayerHTML(player));
            }

            sbIndex.AppendLine("</OL></BODY></HTML>");
            File.WriteAllText(Path.Combine(folder, "index.html"), sbIndex.ToString());
            System.Diagnostics.Process.Start(Path.Combine(folder, "index.html"));
        }
    }
}
