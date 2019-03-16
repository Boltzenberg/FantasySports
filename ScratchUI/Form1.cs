using System;
using System.Collections.Generic;
using System.IO;
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
            this.wbOut.DocumentText = await YahooFantasySports.Services.Http.GetRawDataAsync(new Uri(this.tbUrl.Text.Trim()));
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.wbOut.DocumentText = await YahooFantasySports.Services.Http.GetRawDataAsync(YahooFantasySports.UrlGen.LeagueUrl(YahooFantasySports.Constants.Leagues.Rounders2019));
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
            List<YahooFantasySports.DataModel.Player> players = await YahooFantasySports.DataModel.Player.GetAllPlayers(YahooFantasySports.Constants.Leagues.Rounders2019);

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
    }
}
