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
                sb.AppendLine(player.PlayerName + " on MLB team " + player.MLBTeam + " going for $" + player.CurrentBidPrice + " to " + player.HighestBiddingTeam + " with " + player.TimeLeft + " time remaining.");
            }

            this.tbOutput.Text = sb.ToString();
        }
    }
}
