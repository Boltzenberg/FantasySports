using FantasyAlgorithms.DataModel;
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

            Dictionary<string, Roster> teamsMap = new Dictionary<string, Roster>();
            foreach (var team in league.Teams)
            {
                teamsMap[team.Name] = new Roster(team);
            }

            foreach (var player in league.AllPlayers)
            {
                if (!string.IsNullOrEmpty(player.FantasyTeam))
                {
                    teamsMap[player.FantasyTeam].Players.Add(player);
                }
            }

            List<Roster> teams = new List<Roster>(teamsMap.Values);
            teams.Sort((x, y) => x.Team.Name.CompareTo(y.Team.Name));

            int maxPlayers = 0;
            foreach (var team in teams)
            {
                team.Players.Sort((x, y) => y.AuctionPrice.CompareTo(x.AuctionPrice));
                maxPlayers = Math.Max(maxPlayers, team.Players.Count);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1' style='font-size:12px'><TR><TD>&nbsp;</TD>");
            for (int col = 0; col < teams.Count; col++)
            {
                sb.AppendFormat("<TD>{0}<BR>{1}<BR>{2,12:C2}</TD>", teams[col].Team.Name, teams[col].Team.Owner, teams[col].Team.Budget);
            }
            sb.AppendLine("</TR>");
            for (int row = 0; row < maxPlayers; row++)
            {
                sb.AppendFormat("<TR><TD>{0}</TD>", row + 1);
                for (int col = 0; col < teams.Count; col++)
                {
                    if (row < teams[col].Players.Count)
                    {
                        IPlayer player = teams[col].Players[row];
                        sb.AppendFormat("<TD>{0} ({1}): {2,12:C2}</TD>", player.Name, player.Status, player.AuctionPrice);
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

        private class Roster
        {
            public Team Team { get; private set; }
            public List<IPlayer> Players { get; set; }

            public Roster(Team team)
            {
                this.Team = team;
                this.Players = new List<IPlayer>();
            }
        }
    }
}
