using FantasyAlgorithms;
using FantasyAuction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class StatCenter : Form
    {
        public static readonly List<IStatExtractor> StatExtractors = new List<IStatExtractor>()
        {
            new CountingStatExtractor("Runs", true, Extractors.ExtractBatterRuns),
            new CountingStatExtractor("Home Runs", true, Extractors.ExtractBatterHomeRuns),
            new CountingStatExtractor("RBIs", true, Extractors.ExtractBatterRBIs),
            new CountingStatExtractor("Steals", true, Extractors.ExtractBatterSteals),
            new RatioStatExtractor("OBP", true, Extractors.ExtractBatterHitsPlusWalks, Extractors.ExtractBatterAtBats, Ratios.Divide),
            new CountingStatExtractor("Wins", true, Extractors.ExtractPitcherWins),
            new CountingStatExtractor("Saves", true, Extractors.ExtractPitcherSaves),
            new CountingStatExtractor("Strikeouts", true, Extractors.ExtractPitcherStrikeouts),
            new RatioStatExtractor("ERA", false, Extractors.ExtractPitcherEarnedRuns, Extractors.ExtractPitcherOutsRecorded, Ratios.ERA),
            new RatioStatExtractor("WHIP", false, Extractors.ExtractPitcherWalksPlusHits, Extractors.ExtractPitcherOutsRecorded, Ratios.WHIP)
        };
        
        public StatCenter(League league)
        {
            InitializeComponent();

            LeagueAnalysis leagueAnalysis = LeagueAnalysis.Analyze(league, StatExtractors);

            this.lvStats.BeginUpdate();
            this.lvPoints.BeginUpdate();

            int columnWidth = this.lvPoints.Width / (StatExtractors.Count + 2);
            this.AddColumns(this.lvStats, StatExtractors, columnWidth);
            this.AddColumns(this.lvPoints, StatExtractors, columnWidth);
            this.lvPoints.Columns.Add("Total Points", columnWidth);

            foreach (TeamAnalysis team in leagueAnalysis.Teams)
            {
                float totalPoints = 0;
                ListViewItem statsItem = new ListViewItem(team.Team.Name);
                ListViewItem pointsItem = new ListViewItem(team.Team.Name);
                statsItem.Tag = team;
                pointsItem.Tag = team;
                foreach (IStatExtractor extractor in StatExtractors)
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

        private class StatColumnSorter : IComparer
        {
            private int col;
            private bool moreIsBetter;

            public StatColumnSorter()
            {
                col = 0;
            }

            public StatColumnSorter(int column, bool moreIsBetter)
            {
                this.col = column;
                this.moreIsBetter = moreIsBetter;
            }

            public int Compare(object x, object y)
            {
                string xStr = ((ListViewItem)x).SubItems[col].Text;
                string yStr = ((ListViewItem)y).SubItems[col].Text;

                float xVal, yVal;
                if (float.TryParse(xStr, out xVal) &&
                    float.TryParse(yStr, out yVal))
                {
                    if (this.moreIsBetter)
                    {
                        return yVal.CompareTo(xVal);
                    }
                    else
                    {
                        return xVal.CompareTo(yVal);
                    }
                }

                if (this.moreIsBetter)
                {
                    return String.Compare(yStr, xStr);
                }
                else
                {
                    return String.Compare(xStr, yStr);
                }
            }
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
