using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class StatCenter : Form
    {
        public StatCenter(League league, Func<IPlayer, string> extractTeam)
        {
            InitializeComponent();

            List<IRoster> teams = RosterAnalysis.AssignPlayersToTeams(league, extractTeam);
            RosterAnalysis.AssignStatsAndPoints(teams, League.ScoringStatExtractors);

            this.lvStats.BeginUpdate();
            this.lvPoints.BeginUpdate();

            int columnWidth = this.lvPoints.Width / (League.ScoringStatExtractors.Count + 2);
            this.AddColumns(this.lvStats, League.ScoringStatExtractors, columnWidth);
            this.AddColumns(this.lvPoints, League.ScoringStatExtractors, columnWidth);
            this.lvPoints.Columns.Add("Total Points", columnWidth);

            foreach (IRoster team in teams)
            {
                float totalPoints = 0;
                ListViewItem statsItem = new ListViewItem(team.TeamName);
                ListViewItem pointsItem = new ListViewItem(team.TeamName);
                statsItem.Tag = team;
                pointsItem.Tag = team;
                foreach (IStatExtractor extractor in League.ScoringStatExtractors)
                {
                    statsItem.SubItems.Add(team.Stats[extractor.StatName].ToString());
                    pointsItem.SubItems.Add(team.Points[extractor.StatName].ToString());
                    totalPoints += team.Points[extractor.StatName];
                }
                pointsItem.SubItems.Add(totalPoints.ToString());
                this.lvStats.Items.Add(statsItem);
                this.lvPoints.Items.Add(pointsItem);
            }

            this.lvStats.EndUpdate();
            this.lvPoints.EndUpdate();
        }

        private void AddColumns(ListView lv, List<IStatExtractor> extractors, int columnWidth)
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
                    new TeamStatCenter(team).Show();
                }
            }
        }
    }
}
