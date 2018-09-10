using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            await YahooFantasySports.AuthManager.Instance.InitializeAuthorizationAsync(this.tbAuthCode.Text.Trim());
        }

        private void OnLoad(object sender, EventArgs e)
        {
            YahooFantasySports.SignInDialog dlg = new YahooFantasySports.SignInDialog();
            dlg.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            HttpWebRequest req = WebRequest.CreateHttp(this.tbUrl.Text.Trim());
            await YahooFantasySports.AuthManager.Instance.AuthorizeRequestAsync(req);

            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                this.wbOut.DocumentText = await responseBody.ReadToEndAsync();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            HttpWebRequest req = WebRequest.CreateHttp(YahooFantasySports.UrlGen.LeagueUrl(YahooFantasySports.Constants.Leagues.Rounders2018));
            await YahooFantasySports.AuthManager.Instance.AuthorizeRequestAsync(req);

            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                this.wbOut.DocumentText = await responseBody.ReadToEndAsync();
            }
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
            List<YahooFantasySports.DataModel.Player> players = await YahooFantasySports.DataModel.Player.GetAllPlayers(YahooFantasySports.Constants.Leagues.Rounders2018);

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
