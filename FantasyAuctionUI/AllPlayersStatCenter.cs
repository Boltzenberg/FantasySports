﻿using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class AllPlayersStatCenter : Form
    {
        public AllPlayersStatCenter(League league)
        {
            InitializeComponent();

            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);
            this.lv.BeginUpdate();
            int columnWidth = lv.Width / (lc.ScoringStatExtractors.Count + lc.SupportingStatExtractors.Count + 4);
            lv.Columns.Add("Player Name", columnWidth);
            lv.Columns.Add("Fantasy Team", columnWidth);
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors.Union(lc.SupportingStatExtractors))
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
            lv.Columns.Add("Average Percentile (Batter)", columnWidth);
            lv.Columns.Add("Average Percentile (Pitcher)", columnWidth);

            Dictionary<string, int> statToPlayerCount = new Dictionary<string, int>();
            PercentilePlayerGroupAnalyzer analyzer = new PercentilePlayerGroupAnalyzer();
            foreach (IPlayer player in league.AllPlayers)
            {
                ListViewItem item = new ListViewItem(player.Name);
                item.SubItems.Add(player.FantasyTeam);
                int percentile = 0;
                int statcount = 0;
                foreach (IStatExtractor extractor in lc.ScoringStatExtractors.Union(lc.SupportingStatExtractors))
                {
                    IStatValue value = extractor.Extract(player);
                    if (value != null)
                    {
                        IEnumerable<IPlayer> players = league.Batters;
                        if (!player.IsBatter)
                        {
                            players = league.Pitchers;
                        }
                        percentile += analyzer.GetPercentile(extractor, players, player);
                        statcount++;
                        item.SubItems.Add(value.Value.ToString());
                        if (!statToPlayerCount.ContainsKey(extractor.StatName))
                        {
                            statToPlayerCount[extractor.StatName] = 1;
                        }
                        else
                        {
                            statToPlayerCount[extractor.StatName]++;
                        }
                    }
                    else
                    {
                        item.SubItems.Add(string.Empty);
                    }
                }

                if (player.IsBatter)
                {
                    item.SubItems.Add((percentile / statcount).ToString());
                    item.SubItems.Add(string.Empty);
                }
                else
                {
                    item.SubItems.Add(string.Empty);
                    item.SubItems.Add((percentile / statcount).ToString());
                }
                this.lv.Items.Add(item);
            }

            this.lv.EndUpdate();
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
        }
    }
}
