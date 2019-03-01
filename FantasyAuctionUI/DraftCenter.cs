using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class DraftCenter : Form
    {
        public DraftCenter()
        {
            InitializeComponent();
        }

        public void SetData(List<FantasyAuction.DataModel.Batter> batters, List<FantasyAuction.DataModel.Pitcher> pitchers)
        {
            Dictionary<string, ListViewStats> teams = new Dictionary<string, ListViewStats>();
            foreach (var b in batters)
            {
                if (!string.IsNullOrEmpty(b.FantasyTeam))
                {
                    ListViewStats team;
                    if (!teams.TryGetValue(b.FantasyTeam, out team))
                    {
                        team = new ListViewStats(b.FantasyTeam);
                        teams[b.FantasyTeam] = team;
                    }
                    team.AddBatter(b);
                }
            }

            foreach (var p in pitchers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam))
                {
                    ListViewStats team;
                    if (!teams.TryGetValue(p.FantasyTeam, out team))
                    {
                        team = new ListViewStats(p.FantasyTeam);
                        teams[p.FantasyTeam] = team;
                    }
                    team.AddPitcher(p);
                }
            }

            List<ListViewItem> pointsItems = new List<ListViewItem>();
            this.lvStats.SuspendLayout();
            this.lvStats.Items.Clear();
            foreach (var team in teams.Values)
            {
                this.lvStats.Items.Add(team.GetStatsListViewItem());
                pointsItems.Add(team.GetStatsListViewItem());
            }
            this.lvStats.ResumeLayout();

            for (int i = 1; i < this.lvPoints.Columns.Count - 1; i++)
            {
                object tag = this.lvPoints.Columns[i].Tag;
                pointsItems.Sort(new StatColumnSorter(i, tag == null || tag.ToString() != "asc").Compare);
                for (int j = 0; j < pointsItems.Count;)
                {
                    int countOfEqual = 1;
                    int totalPoints = pointsItems.Count - j;
                    while (j + countOfEqual < pointsItems.Count && pointsItems[j + countOfEqual - 1].SubItems[i].Text == pointsItems[j + countOfEqual].SubItems[i].Text)
                    {
                        totalPoints += pointsItems.Count - (j + countOfEqual);
                        countOfEqual++;
                    }
                    float pointsPer = (float)totalPoints / countOfEqual;
                    for (int k = 0; k < countOfEqual; k++)
                    {
                        pointsItems[j + k].SubItems[i].Text = pointsPer.ToString();
                    }
                    j += countOfEqual;
                }
            }

            for (int i = 0; i < pointsItems.Count; i++)
            {
                float total = 0f;
                for (int j = 1; j < pointsItems[i].SubItems.Count; j++)
                {
                    float current;
                    if (float.TryParse(pointsItems[i].SubItems[j].Text, out current))
                    {
                        total += current;
                    }
                }
                pointsItems[i].SubItems.Add(total.ToString());
            }

            this.lvPoints.SuspendLayout();
            this.lvPoints.Items.Clear();
            this.lvPoints.Items.AddRange(pointsItems.ToArray());
            this.lvPoints.ResumeLayout();
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
        }

        private class ListViewStats
        {
            public string TeamName { get; private set; }
            public List<FantasyAuction.DataModel.Batter> Batters { get; private set; }
            public List<FantasyAuction.DataModel.Pitcher> Pitchers { get; private set; }
            public int BatterRuns { get; private set; }
            public int BatterHomeRuns { get; private set; }
            public int BatterRBIs { get; private set; }
            public int BatterSteals { get; private set; }
            public int BatterWalksPlusHits { get; private set; }
            public int BatterAtBats { get; private set; }
            public int PitcherWins { get; private set; }
            public int PitcherSaves { get; private set; }
            public int PitcherStrikeouts { get; private set; }
            public int PitcherOutsRecorded { get; private set; }
            public int PitcherWalks { get; private set; }
            public int PitcherHits { get; private set; }
            public int PitcherEarnedRuns { get; private set; }

            public ListViewStats(string teamName)
            {
                this.TeamName = teamName;
                this.Batters = new List<FantasyAuction.DataModel.Batter>();
                this.Pitchers = new List<FantasyAuction.DataModel.Pitcher>();
            }

            public void AddBatter(FantasyAuction.DataModel.Batter batter)
            {
                this.BatterAtBats += batter.ProjectedAB;
                this.BatterHomeRuns += batter.ProjectedHR;
                this.BatterRBIs += batter.ProjectedRBI;
                this.BatterRuns += batter.ProjectedR;
                this.BatterSteals += batter.ProjectedSB;
                this.BatterWalksPlusHits += batter.ProjectedWalksPlusHits;
                this.Batters.Add(batter);
            }

            public void AddPitcher(FantasyAuction.DataModel.Pitcher pitcher)
            {
                this.PitcherWins += pitcher.ProjectedW;
                this.PitcherSaves += pitcher.ProjectedSV;
                this.PitcherStrikeouts += pitcher.ProjectedK;
                this.PitcherOutsRecorded += pitcher.ProjectedOutsRecorded;
                this.PitcherWalks += pitcher.ProjectedWalks;
                this.PitcherHits += pitcher.ProjectedHits;
                this.PitcherEarnedRuns += pitcher.ProjectedER;
                this.Pitchers.Add(pitcher);
            }

            public ListViewItem GetStatsListViewItem()
            {
                string[] values = new string[11];
                values[0] = this.TeamName;
                values[1] = this.BatterRuns.ToString();
                values[2] = this.BatterHomeRuns.ToString();
                values[3] = this.BatterRBIs.ToString();
                values[4] = this.BatterSteals.ToString();
                values[5] = this.BatterAtBats > 0 ? ((float)this.BatterWalksPlusHits / (float)this.BatterAtBats).ToString() : "0";
                values[6] = this.PitcherWins.ToString();
                values[7] = this.PitcherSaves.ToString();
                values[8] = this.PitcherStrikeouts.ToString();
                values[9] = (this.PitcherEarnedRuns / (float)(this.PitcherOutsRecorded / 27)).ToString();
                values[10] = ((this.PitcherHits + this.PitcherWalks) / (float)(this.PitcherOutsRecorded / 3)).ToString();
                return new ListViewItem(values);
            }

            private static int SumAcross<T>(Func<T, int> getStat, List<T> items)
            {
                int total = 0;
                foreach (T item in items)
                {
                    total += getStat(item);
                }
                return total;
            }
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
