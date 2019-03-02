using FantasyAuction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class RosterCenter : Form
    {
        public RosterCenter(League league)
        {
            InitializeComponent();

            Dictionary<string, Team> teamsMap = new Dictionary<string, Team>();
            foreach (string team in league.Teams)
            {
                teamsMap[team] = new Team(team);
            }

            foreach (var player in league.AllPlayers)
            {
                if (!string.IsNullOrEmpty(player.FantasyTeam))
                {
                    teamsMap[player.FantasyTeam].Players.Add(string.Format("{0}: {1,12:C2}", player.Name, player.AuctionPrice));
                }
            }

            List<Team> teams = new List<Team>(teamsMap.Values);
            teams.Sort((x, y) => x.Name.CompareTo(y.Name));

            int maxPlayers = 0;
            foreach (var team in teams)
            {
                team.Players.Sort();
                maxPlayers = Math.Max(maxPlayers, team.Players.Count);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1' style='font-size:12px'><TR><TD>&nbsp;</TD>");
            for (int col = 0; col < teams.Count; col++)
            {
                sb.AppendFormat("<TD>{0}</TD>", teams[col].Name);
            }
            sb.AppendLine("</TR>");
            for (int row = 0; row < maxPlayers; row++)
            {
                sb.AppendFormat("<TR><TD>{0}</TD>", row + 1);
                for (int col = 0; col < teams.Count; col++)
                {
                    if (row < teams[col].Players.Count)
                    {
                        sb.AppendFormat("<TD>{0}</TD>", teams[col].Players[row]);
                    }
                    else
                    {
                        sb.Append("<TD>&nbsp;</TD>");
                    }
                }
                sb.AppendLine("</TR>");
            }
            sb.AppendLine("</TABLE></BODY></HTML>");
            this.wb.DocumentText = sb.ToString();
        }

        private class Team
        {
            public string Name { get; private set; }
            public List<string> Players { get; set; }

            public Team(string name)
            {
                this.Name = name;
                this.Players = new List<string>();
            }
        }
    }
}
