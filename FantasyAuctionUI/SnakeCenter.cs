using FantasyAlgorithms.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class SnakeCenter : Form
    {
        private League league;
        private string fileName;
        private string myTeam;
        private List<string> teamsForAssignment;

        public SnakeCenter(League league, string fileName, string myTeam)
        {
            InitializeComponent();
            this.Text = string.Format("League Center for {0}", myTeam);
            this.league = league;
            this.fileName = fileName;
            this.myTeam = myTeam;
            this.teamsForAssignment = new List<string>(this.league.Teams.Select(t => t.Name));
            this.teamsForAssignment.Add(string.Empty);
            this.teamsForAssignment.Sort();

            UIUtilities.PrepRosterAnalysisPlayerList(this.league, this.lvTarget);
            UIUtilities.PrepStatsPlayerList(this.league, this.lvStats);
            UIUtilities.PrepPointsPlayerList(this.league, this.lvPlayers);
            this.UpdatePlayerList();
        }

        private void OnWordWheelChanged(object sender, System.EventArgs e)
        {
            this.UpdatePlayerList();
        }

        private bool PlayerPassesFilter(IPlayer player)
        {
            if (player != null && !string.IsNullOrEmpty(this.tbWordWheel.Text))
            {
                return player.Name.ToLowerInvariant().StartsWith(this.tbWordWheel.Text.ToLowerInvariant());
            }

            return true;
        }

        private void UpdatePlayerList()
        {
            UIUtilities.UpdateRosterAnalysisPlayerList(this.league, this.myTeam, this.lvTarget, this.PlayerPassesFilter);
            UIUtilities.UpdateStatsPlayerList(this.league, this.lvStats, this.PlayerPassesFilter);
            UIUtilities.UpdatePointsPlayerList(this.league, this.lvPlayers, this.PlayerPassesFilter);
        }

        private void OnPlayerSelected(object sender, System.EventArgs e)
        {
            PromptFromList prompt = new PromptFromList(string.Format("Which team drafted {0}?", this.lvTarget.SelectedItems[0].Text), this.teamsForAssignment);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                IPlayer clonedPlayer = this.lvTarget.SelectedItems[0].Tag as IPlayer;
                if (clonedPlayer != null)
                {
                    IPlayer player = this.league.AllPlayers.FirstOrDefault(p => p.ESPNId == clonedPlayer.ESPNId);
                    if (player != null)
                    {
                        player.FantasyTeam = prompt.SelectedItem;
                        this.league.Save(this.fileName);
                        this.UpdatePlayerList();
                    }
                }
            }
        }
    }
}
