using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class PlayerGroupCenter : Form
    {
        private List<IPlayer> allPlayers;
        private LeagueConstants constants;
        private IPlayer currentPlayer;

        public PlayerGroupCenter(string groupDescription, LeagueConstants constants, IEnumerable<IPlayer> allPlayers)
        {
            InitializeComponent();
            this.Text = groupDescription;
            this.constants = constants;
            this.allPlayers = new List<IPlayer>(allPlayers);
            this.allPlayers.Sort((x, y) => x.Name.CompareTo(y.Name));
            this.tbWordWheel.Text = string.Empty;
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.ToArray());
            this.currentPlayer = null;
            this.UpdateWebView();
        }

        private void OnWordWheel(object sender, EventArgs e)
        {
            this.lbPlayers.BeginUpdate();
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.Where(x => x.Name.StartsWith(this.tbWordWheel.Text, StringComparison.OrdinalIgnoreCase)).ToArray());
            this.lbPlayers.EndUpdate();
        }

        private void UpdateWebView()
        {
            PercentilePlayerGroupAnalyzer analyzer = new PercentilePlayerGroupAnalyzer();
            string title = "Analysis of " + this.Text;
            IEnumerable<IStatExtractor> extractors = this.constants.ScoringStatExtractors;
            if (this.currentPlayer != null)
            {
                title += " on player " + this.currentPlayer.Name;
                if (this.currentPlayer is Batter)
                {
                    extractors = this.constants.BattingScoringStatExtractors;
                }
                else
                {
                    extractors = this.constants.PitchingScoringStatExtractors;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><BODY><H1>");
            sb.Append(title);
            sb.Append("</H1><TABLE BORDER=1><TR><TD>Stat Name</TD><TD>Player's Stat Value</TD><TD>Max Value</TD><TD>Min Value</TD><TD>Graph</TD><TD>Player Percentile</TD></TR>");
            foreach (IStatExtractor extractor in extractors)
            {
                using (PlayerGroupAnalysis analysis = analyzer.Analyze(this.Text, extractor, this.allPlayers, this.currentPlayer))
                {
                    byte[] img;
                    using (MemoryStream stm = new MemoryStream())
                    {
                        analysis.Graph.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img = stm.ToArray();
                    }
                    sb.AppendLine($"<TR><TD>{analysis.Stat}</TD><TD>{analysis.PlayerStatValue}</TD><TD>{analysis.MaxStatValue}</TD><TD>{analysis.MinStatValue}</TD><TD><img src=\"data:image/jpg;base64,{Convert.ToBase64String(img)}\"/></TD><TD>{analysis.PlayerPercentile}</TD></TR>");
                }
            }
            sb.AppendLine("</TABLE></BODY></HTML>");
            this.wbOut.DocumentText = sb.ToString();
        }

        private void OnPlayerSelected(object sender, EventArgs e)
        {
            this.currentPlayer = this.lbPlayers.SelectedItem as IPlayer;
            this.UpdateWebView();
        }
    }
}
