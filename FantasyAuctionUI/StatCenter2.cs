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
    public partial class StatCenter2 : Form
    {
        public StatCenter2(League league)
        {
            InitializeComponent();

            List<IStatExtractor> extractors = new List<IStatExtractor>();
            extractors.Add(new CountingStatExtractor("Runs", true, Extractors.ExtractBatterRuns));
            extractors.Add(new CountingStatExtractor("Home Runs", true, Extractors.ExtractBatterHomeRuns));
            extractors.Add(new CountingStatExtractor("RBIs", true, Extractors.ExtractBatterRBIs));
            extractors.Add(new CountingStatExtractor("Steals", true, Extractors.ExtractBatterSteals));
            extractors.Add(new RatioStatExtractor("OBP", true, Extractors.ExtractBatterHitsPlusWalks, Extractors.ExtractBatterAtBats, Ratios.Divide));
            extractors.Add(new CountingStatExtractor("Wins", true, Extractors.ExtractPitcherWins));
            extractors.Add(new CountingStatExtractor("Saves", true, Extractors.ExtractPitcherSaves));
            extractors.Add(new CountingStatExtractor("Strikeouts", true, Extractors.ExtractPitcherStrikeouts));
            extractors.Add(new RatioStatExtractor("ERA", false, Extractors.ExtractPitcherEarnedRuns, Extractors.ExtractPitcherOutsRecorded, Ratios.ERA));
            extractors.Add(new RatioStatExtractor("WHIP", false, Extractors.ExtractPitcherWalksPlusHits, Extractors.ExtractPitcherOutsRecorded, Ratios.WHIP));
            LeagueAnalysis leagueAnalysis = LeagueAnalysis.Analyze(league, extractors);

            this.lvStats.BeginUpdate();
            this.lvPoints.BeginUpdate();

            int columnWidth = this.lvPoints.Width / (extractors.Count + 2);
            this.AddColumns(this.lvStats, extractors, columnWidth);
            this.AddColumns(this.lvPoints, extractors, columnWidth);
            this.lvPoints.Columns.Add("Total Points", columnWidth);

            foreach (TeamAnalysis team in leagueAnalysis.Teams)
            {
                float totalPoints = 0;
                ListViewItem statsItem = new ListViewItem(team.Team.Name);
                ListViewItem pointsItem = new ListViewItem(team.Team.Name);
                foreach (IStatExtractor extractor in extractors)
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
    }
}
