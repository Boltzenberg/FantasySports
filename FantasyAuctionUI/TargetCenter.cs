using FantasyAlgorithms.DataModel;
using System;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class TargetCenter : Form
    {
        private League league;

        public TargetCenter(League league)
        {
            InitializeComponent();
            this.league = league;
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            foreach (Team team in this.league.Teams)
            {
                this.cbTeams.Items.Add(team.Name);
            }

            UIUtilities.PrepRosterAnalysisPlayerList(this.league, this.lvPlayers);
        }

        private void OnTeamSelected(object sender, EventArgs e)
        {
            UIUtilities.UpdateRosterAnalysisPlayerList(this.league, this.cbTeams.Items[this.cbTeams.SelectedIndex].ToString(), this.lvPlayers, this.PlayerPassesFilters);
        }

        private bool PlayerPassesFilters(IPlayer player)
        {
            Batter b = player as Batter;
            if (cb1B.Checked && b != null && b.Is1B) return true;
            if (cb2B.Checked && b != null && b.Is2B) return true;
            if (cb3B.Checked && b != null && b.Is3B) return true;
            if (cbSS.Checked && b != null && b.IsSS) return true;
            if (cbOF.Checked && b != null && b.IsOF) return true;
            if (cbC.Checked && b != null && b.IsC) return true;
            if (cbUtil.Checked && b != null) return true;

            Pitcher p = player as Pitcher;
            if (cbSP.Checked && p != null && p.IsSP) return true;
            if (cbRP.Checked && p != null && p.IsRP) return true;

            return false;
        }
    }
}
