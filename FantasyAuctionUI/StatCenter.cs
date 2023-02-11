using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class StatCenter : Form
    {
        private League league;

        public StatCenter(League league, Func<IPlayer, string> extractTeam)
        {
            InitializeComponent();

            this.league = league;
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);
            List<IRoster> teams = RosterAnalysis.AssignPlayersToTeams(league, extractTeam);
            RosterAnalysis.AssignStatsAndPoints(teams, lc.ScoringStatExtractors);

            this.lvStats.BeginUpdate();
            this.lvPoints.BeginUpdate();

            int columnWidth = this.lvPoints.Width / (lc.ScoringStatExtractors.Count + 2);
            this.AddColumns(this.lvStats, lc.ScoringStatExtractors, columnWidth);
            this.AddColumns(this.lvPoints, lc.ScoringStatExtractors, columnWidth);
            this.lvPoints.Columns.Add("Total Points", columnWidth);

            foreach (IRoster team in teams)
            {
                float totalPoints = 0;
                ListViewItem statsItem = new ListViewItem(team.TeamName);
                ListViewItem pointsItem = new ListViewItem(team.TeamName);
                statsItem.Tag = team;
                pointsItem.Tag = team;
                foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
                {
                    statsItem.SubItems.Add(team.Stats.ContainsKey(extractor.StatName) ? team.Stats[extractor.StatName].ToString() : "N/A");
                    pointsItem.SubItems.Add(team.Points.ContainsKey(extractor.StatName) ? team.Points[extractor.StatName].ToString() : "N/A");
                    totalPoints += team.Points[extractor.StatName];
                }
                pointsItem.SubItems.Add(totalPoints.ToString());
                this.lvStats.Items.Add(statsItem);
                this.lvPoints.Items.Add(pointsItem);
            }

            this.lvStats.EndUpdate();
            this.lvPoints.EndUpdate();
        }

        private void AddColumns(ListView lv, IReadOnlyList<IStatExtractor> extractors, int columnWidth)
        {
            lv.Columns.Add("Team Name", columnWidth);
            foreach (IStatExtractor extractor in extractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                if (!extractor.MoreIsBetter)
                {
                    column.Tag = "asc";
                }
                lv.Columns.Add(column);
            }
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
        }

        private void OnLaunchTeamStatCenter(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv.SelectedItems.Count == 1)
            {
                TeamAnalysis team = lv.SelectedItems[0].Tag as TeamAnalysis;
                if (team != null)
                {
                    new TeamStatCenter(this.league, team).Show();
                }
            }
        }
    }
}
