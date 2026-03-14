using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClaudeExporter
{
    public partial class Form1 : Form
    {
        NirDriver driver = null;
        public Form1()
        {
            InitializeComponent();
            this.driver = new NirDriver(this.webview);
        }

        private async void OnExportRoster(object sender, EventArgs e)
        {
            var roster = await this.driver.GetTeamRosters();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Rosters:");

            foreach (var team in roster.Keys)
            {
                sb.AppendLine("'" + team.Name + "' with remaining balance " + team.Balance);
                foreach (var player in roster[team])
                {
                    sb.AppendLine(player.Name + " on MLB team " + player.MLBTeamName + " filling positions " + string.Join(", ", player.Positions) + " for $" + player.Price);
                }
                sb.AppendLine();
            }

            this.tbOutput.Text = sb.ToString();
        }

        private async void OnExportAuction(object sender, EventArgs e)
        {
            var auction = await this.driver.GetPlayersUpForAuction();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Auctions:");

            foreach (var player in auction)
            {
                string topperSuffix = string.Empty;
                if (!string.IsNullOrEmpty(player.PossibleTopper))
                {
                    topperSuffix = string.Format(" with {0} as a possible topper", player.PossibleTopper);
                }

                sb.AppendFormat("{0} on MLB team {1} going for ${2} to {3} with {4} time remaining{5}.",
                    player.PlayerName,
                    player.MLBTeam,
                    player.CurrentBidPrice,
                    player.HighestBiddingTeam,
                    player.TimeLeft,
                    topperSuffix);
                sb.AppendLine();
            }

            this.tbOutput.Text = sb.ToString();
        }
    }
}
