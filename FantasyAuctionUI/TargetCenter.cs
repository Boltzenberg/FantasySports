using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
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

            this.lvPlayers.BeginUpdate();
            int columnWidth = this.lvPlayers.Width / (lc.ScoringStatExtractors.Count + 2);
            this.lvPlayers.Columns.Add("Player Name", columnWidth);
            this.lvPlayers.Columns.Add("Stat Delta");
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                this.lvPlayers.Columns.Add(column);
            }
            this.lvPlayers.EndUpdate();
        }

        private void OnTeamSelected(object sender, EventArgs e)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            List<IRoster> baselineTeams = RosterAnalysis.AssignPlayersToTeams(this.league, p => p.FantasyTeam);
            RosterAnalysis.AssignStatsAndPoints(baselineTeams, lc.ScoringStatExtractors);
            IRoster baselineTeam = baselineTeams.Find(t => t.TeamName == this.cbTeams.Items[this.cbTeams.SelectedIndex].ToString());
            if (baselineTeam == null)
            {
                return;
            }

            this.lvPlayers.BeginUpdate();
            this.lvPlayers.Items.Clear();
            League l = this.league.Clone();
            foreach (IPlayer p in l.AllPlayers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam) || !this.PlayerPassesFilters(p))
                {
                    continue;
                }

                p.FantasyTeam = baselineTeam.TeamName;

                List<IRoster> teams = RosterAnalysis.AssignPlayersToTeams(l, pb => pb.FantasyTeam);
                RosterAnalysis.AssignStatsAndPoints(teams, lc.ScoringStatExtractors);
                IRoster team = teams.Find(t => t.TeamName == baselineTeam.TeamName);

                ListViewItem item = new ListViewItem(p.Name);
                item.SubItems.Add(string.Empty); // total delta
                float totalDelta = 0f;
                foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
                {
                    float delta = team.Points[extractor.StatName] - baselineTeam.Points[extractor.StatName];
                    totalDelta += delta;
                    item.SubItems.Add(delta.ToString());
                }
                item.SubItems[1].Text = totalDelta.ToString();
                this.lvPlayers.Items.Add(item);

                p.FantasyTeam = string.Empty;
            }
            this.lvPlayers.EndUpdate();
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
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
