using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            sb.AppendLine("# Rosters #");
            foreach (var team in roster.Keys)
            {
                sb.AppendFormat(" ## {0} with remaining balance {1} ##", team.Name, team.Balance);
                sb.AppendLine();
                sb.AppendLine("| Player Name | MLB Team | Positions | Price |");
                sb.AppendLine("|-------------|----------|-----------|-------|");
                foreach (var player in roster[team])
                {
                    sb.AppendFormat("| {0} | {1} | {2} | {3} |", player.Name, player.MLBTeamName, String.Join(", ", player.Positions), player.Price);
                    sb.AppendLine();
                }
                sb.AppendLine();
            }

            this.tbOutput.Text = sb.ToString();

            await this.wvMarkdown.EnsureCoreWebView2Async();

            string html = $@"
                <html>
                <head>
                <script src=""https://cdn.jsdelivr.net/npm/marked/marked.min.js""></script>
                </head>
                <body>
                <div id='content'></div>
                <script>
                document.getElementById('content').innerHTML = marked.parse({JsonSerializer.Serialize(sb.ToString())});
                </script>
                </body>
                </html>";
            this.wvMarkdown.NavigateToString(html);
        }

        private async void OnExportAuction(object sender, EventArgs e)
        {
            var auction = await this.driver.GetPlayersUpForAuction();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Auctions #");

            sb.AppendLine("| Player Name | MLB Team | Current Bid Price | Highest Bidding Team | Auction Ends | Team with Topper |");
            sb.AppendLine("|-------------|----------|-------------------|----------------------|--------------|------------------|");
            foreach (var player in auction)
            {
                sb.AppendFormat("| {0} | {1} | {2} | {3} | {4} | {5} |", player.PlayerName, player.MLBTeam, player.CurrentBidPrice, player.HighestBiddingTeam, player.AuctionEnds, player.PossibleTopper);
                sb.AppendLine();
            }

            this.tbOutput.Text = sb.ToString();
            await this.wvMarkdown.EnsureCoreWebView2Async();

            string html = $@"
                <html>
                <head>
                <script src=""https://cdn.jsdelivr.net/npm/marked/marked.min.js""></script>
                </head>
                <body>
                <div id='content'></div>
                <script>
                document.getElementById('content').innerHTML = marked.parse({JsonSerializer.Serialize(sb.ToString())});
                </script>
                </body>
                </html>";
            this.wvMarkdown.NavigateToString(html);
        }
    }
}
