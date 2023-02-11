using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class LeagueSettings : Form
    {
        private League league;

        public LeagueSettings(League league)
        {
            InitializeComponent();

            this.league = league;

            foreach (Team team in this.league.Teams)
            {
                ListViewItem item = new ListViewItem(team.Name);
                item.SubItems.Add(team.Owner);
                item.SubItems.Add(team.Budget.ToString());
                item.Tag = team;
                this.lv.Items.Add(item);
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            Dictionary<string, string> oldToNewName = new Dictionary<string, string>();
            foreach (ListViewItem item in this.lv.Items)
            {
                float budget;
                if (float.TryParse(item.SubItems[2].Text.Trim(), out budget))
                {
                    Team team = item.Tag as Team;
                    string newName = item.SubItems[0].Text.Trim();
                    string newOwner = item.SubItems[1].Text.Trim();
                    if (team.Name != newName)
                    {
                        oldToNewName[team.Name] = newName;
                    }
                    team.Name = newName;
                    team.Owner = newOwner;
                    team.Budget = budget;
                }
            }

            foreach (string oldName in oldToNewName.Keys)
            {
                string newName = oldToNewName[oldName];
                foreach (IPlayer player in this.league.AllPlayers)
                {
                    if (player.FantasyTeam == oldName)
                    {
                        player.FantasyTeam = newName;
                    }
                }
            }
        }
    }
}
