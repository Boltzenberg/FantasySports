using NirSiteLib;
using NirSiteLib.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ScratchUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await YahooFantasySports.Services.AuthManager.Instance.InitializeAuthorizationAsync(this.tbAuthCode.Text.Trim());
        }

        private void OnLoad(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(YahooFantasySports.Services.AuthManager.GetAuthUrl().AbsoluteUri);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.wbOut.DocumentText = await YahooFantasySports.Services.Http.GetRawDataAsync(new Uri(this.tbUrl.Text.Trim()));
            }
            catch (Exception ex)
            {
                this.wbOut.DocumentText = ex.ToString();
            }
        }

        private async void OnGetMyTeam(object sender, EventArgs e)
        {
            string myTeamUrl = "https://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1/games;game_keys=mlb/teams";
            try
            {
                this.wbOut.DocumentText = await YahooFantasySports.Services.Http.GetRawDataAsync(new Uri(myTeamUrl));
            }
            catch (Exception ex)
            {
                this.wbOut.DocumentText = ex.ToString();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.wbOut.DocumentText = await YahooFantasySports.Services.Http.GetRawDataAsync(YahooFantasySports.UrlGen.LeagueUrl(YahooFantasySports.Constants.Leagues.Rounders2024));
        }

        private string GetPositionList(YahooFantasySports.DataModel.Player player)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<UL>");
            foreach (string position in player.Positions)
            {
                sb.AppendFormat("<LI>{0}</LI>", position);
            }
            sb.Append("</UL>");
            return sb.ToString();
        }

        private string GetStats(YahooFantasySports.DataModel.Player player)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<UL>");
            foreach (KeyValuePair<int, string> kvp in player.Stats)
            {
                sb.AppendFormat("<LI>{0}: {1}</LI>", kvp.Key, kvp.Value);
            }
            sb.Append("</UL>");
            return sb.ToString();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            List<YahooFantasySports.DataModel.Player> players = await YahooFantasySports.DataModel.Player.GetAllPlayers(YahooFantasySports.Constants.Leagues.Rounders2025);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE><TR><TH>Key</TH><TH>Id</TH><TH>Name</TH><TH>Positions</TH><TH>Stats</TH></TR>");
            foreach (var player in players)
            {
                sb.AppendFormat("<TR><TD>{0}</TD><TD>{1}</TD><TD>{2}</TD><TD>{3}</TD><TD>{4}</TD></TR>", player.Key, player.Id, player.Name, GetPositionList(player), GetStats(player));
                sb.AppendLine();
            }
            sb.Append("</TABLE></BODY></HTML>");
            this.wbOut.DocumentText = sb.ToString();
        }

        public class YahooPosition
        {
            public string Position;
            public int PlayerCount;
            public float PercentMatch;
        }

        public class YahooPlayer
        {
            public string Name;
            public string[] Positions;
        }

        public class ESPNPositionMap
        {
            public int ESPNPosition;
            public YahooPosition[] YahooPosition;
            public YahooPlayer[] Players;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> yDM = new Dictionary<string, List<string>>();
            HashSet<string> multipleNames = new HashSet<string>();
            foreach (YahooFantasySports.DataModel.Player yPlayer in await YahooFantasySports.DataModel.Player.GetAllPlayers(YahooFantasySports.Constants.Leagues.Rounders2019))
            {
                if (multipleNames.Contains(yPlayer.Name))
                {
                    continue;
                }

                if (yDM.ContainsKey(yPlayer.Name))
                {
                    multipleNames.Add(yPlayer.Name);
                    yDM.Remove(yPlayer.Name);
                    continue;
                }

                yDM[yPlayer.Name] = new List<string>(yPlayer.Positions.Where(p => YahooFantasySports.Constants.Positions.Individual.Contains(p)));
            }

            Dictionary<string, List<int>> eDM = new Dictionary<string, List<int>>();
            foreach (ESPNProjections.Player ePlayer in ESPNProjections.Player.LoadProjections())
            {
                if (multipleNames.Contains(ePlayer.FullName))
                {
                    continue;
                }

                if (eDM.ContainsKey(ePlayer.FullName))
                {
                    multipleNames.Add(ePlayer.FullName);
                    eDM.Remove(ePlayer.FullName);
                    continue;
                }

                eDM[ePlayer.FullName] = new List<int>(ePlayer.Positions);
            }

            Dictionary<string, int> mapping = FantasyAlgorithms.DataSourceMerge.MergeOnName(eDM, yDM);

            StringBuilder sb = new StringBuilder("<HTML><BODY><TABLE BORDER='1'><TR><TD>Yahoo Position</TD><TD>ESPN Position</TD></TR>");
            foreach (string yp in mapping.Keys)
            {
                sb.AppendFormat("<TR><TD>{0}</TD><TD>{1}</TD></TR>", yp, mapping[yp]);
            }
            sb.Append("</TABLE></BODY></HTML>");

            this.wbOut.DocumentText = sb.ToString();
        }

        private void OnLaunchNirSite(object sender, EventArgs e)
        {
            using (NirDriver driver = new NirDriver())
            {
                Dictionary<string, List<RosteredPlayer>> rosters = driver.GetTeamRosters();
                List<BidItem> bidItems = driver.GetPlayersUpForAuction();
                StringBuilder sb = new StringBuilder("<HTML><BODY>");
                sb.AppendLine("<TABLE><TR><TD>Player Name</TD><TD>MLB Team</TD><TD>Rank</TD><TD>Preseason Rank</TD><TD>Positions</TD><TD>Highest Bidding Team</TD><TD>Current Bid Price</TD><TD>Old Price</TD><TD>Time Left</TD><TD>Possible Topper</TD><TD>Yahoo ID</TD></TR>");
                foreach (BidItem item in bidItems)
                {
                    StringBuilder positions = new StringBuilder(item.Positions[0]);
                    for (int i = 1; i < item.Positions.Count; i++)
                    {
                        positions.AppendFormat(", {0}", item.Positions[i]);
                    }

                    sb.AppendFormat("<TR><TD>{0}</TD><TD>{1}</TD><TD>{2}</TD><TD>{3}</TD><TD>{4}</TD><TD>{5}</TD><TD>${6}</TD><TD>${7}</TD><TD>{8}</TD><TD>{9}</TD><TD>{10}</TD></TR>",
                        item.PlayerName,
                        item.MLBTeam,
                        item.Rank,
                        item.PreseasonRank,
                        positions.ToString(),
                        item.HighestBiddingTeam,
                        item.CurrentBidPrice,
                        item.OldPrice,
                        item.TimeLeft,
                        item.PossibleTopper,
                        item.YahooId);
                }
                sb.AppendLine("</TABLE>");
                foreach (string team in rosters.Keys)
                {
                    sb.AppendFormat("<H1>{0}</H1>", team);
                    sb.Append("<OL>");
                    foreach (RosteredPlayer player in rosters[team])
                    {
                        StringBuilder positions = new StringBuilder(player.Positions[0]);
                        for (int i = 1; i < player.Positions.Count; i++)
                        {
                            positions.AppendFormat(", {0}", player.Positions[i]);
                        }
                        sb.AppendFormat("<LI>{0} - {1}, {2}, ${3}</LI>", player.Name, player.MLBTeamName, positions.ToString(), player.Price);
                    }
                    sb.AppendLine("</OL>");
                }
                sb.AppendLine("</BODY></HTML>");
                this.wbOut.DocumentText = sb.ToString();
            }
        }
    }
}
