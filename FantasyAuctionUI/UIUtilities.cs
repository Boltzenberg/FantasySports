using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public static class UIUtilities
    {
        public static void PrepRosterAnalysisPlayerList(League league, ListView lv)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            lv.ColumnClick += OnColumnClicked;

            lv.BeginUpdate();
            int columnWidth = lv.Width / (lc.ScoringStatExtractors.Count + 2);
            lv.Columns.Add("Player Name", columnWidth);
            lv.Columns.Add("Stat Delta");
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                lv.Columns.Add(column);
            }
            lv.EndUpdate();
        }

        public static void UpdateRosterAnalysisPlayerList(League league, string myTeam, ListView lv, Func<IPlayer, bool> playerPassesFilter)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            List<IRoster> baselineTeams = RosterAnalysis.AssignPlayersToTeams(league, p => p.FantasyTeam);
            RosterAnalysis.AssignStatsAndPoints(baselineTeams, lc.ScoringStatExtractors);
            IRoster baselineTeam = baselineTeams.Find(t => t.TeamName == myTeam);
            if (baselineTeam == null)
            {
                return;
            }

            lv.BeginUpdate();
            lv.Items.Clear();
            League l = league.Clone();
            foreach (IPlayer p in l.AllPlayers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam) || !playerPassesFilter(p))
                {
                    continue;
                }

                p.FantasyTeam = baselineTeam.TeamName;

                List<IRoster> teams = RosterAnalysis.AssignPlayersToTeams(l, pb => pb.FantasyTeam);
                RosterAnalysis.AssignStatsAndPoints(teams, lc.ScoringStatExtractors);
                IRoster team = teams.Find(t => t.TeamName == baselineTeam.TeamName);

                ListViewItem item = new ListViewItem(p.Name);
                item.Tag = p;
                item.SubItems.Add(string.Empty); // total delta
                float totalDelta = 0f;
                foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
                {
                    float delta = team.Points[extractor.StatName] - baselineTeam.Points[extractor.StatName];
                    totalDelta += delta;
                    item.SubItems.Add(delta.ToString());
                }
                item.SubItems[1].Text = totalDelta.ToString();
                lv.Items.Add(item);

                p.FantasyTeam = string.Empty;
            }
            lv.EndUpdate();
        }

        public static void PrepStatsPlayerList(League league, ListView lv)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            lv.ColumnClick += OnColumnClicked;

            lv.BeginUpdate();
            int columnWidth = lv.Width / (lc.ScoringStatExtractors.Count + 2);
            lv.Columns.Add("Player Name", columnWidth);
            lv.Columns.Add("Player Status", columnWidth);
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
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
            lv.EndUpdate();
        }

        public static void UpdateStatsPlayerList(League league, ListView lv, Func<IPlayer, bool> playerPassesFilter)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);
            lv.BeginUpdate();
            foreach (IPlayer player in league.AllPlayers)
            {
                if (playerPassesFilter(player))
                {
                    ListViewItem item = new ListViewItem(player.Name);
                    item.SubItems.Add(player.Status);
                    foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
                    {
                        IStatValue value = extractor.Extract(player);
                        if (value != null)
                        {
                            item.SubItems.Add(value.Value.ToString());
                        }
                        else
                        {
                            item.SubItems.Add(string.Empty);
                        }
                    }
                    lv.Items.Add(item);
                }
            }
            lv.EndUpdate();
        }

        public static void PrepPointsPlayerList(League league, ListView lv)
        {
            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);

            lv.ColumnClick += OnColumnClicked;

            lv.BeginUpdate();
            int columnWidth = lv.Width / (lc.ScoringStatExtractors.Count + 2);
            lv.Columns.Add("Team Name", columnWidth);
            lv.Columns.Add("Total Points", columnWidth);
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
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

        public static void UpdatePointsPlayerList(League league, ListView lv, Func<IPlayer, bool> playerPassesFilter)
        {
            List<IRoster> teams = new List<IRoster>();
            foreach (IPlayer player in league.AllPlayers)
            {
                if (string.IsNullOrEmpty(player.FantasyTeam) && playerPassesFilter(player))
                {
                    Roster r = new Roster(player.Name);
                    r.AllPlayers.Add(player);
                    teams.Add(r);
                }
            }

            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);
            RosterAnalysis.AssignStatsAndPoints(teams, lc.ScoringStatExtractors);

            lv.BeginUpdate();
            lv.Items.Clear();
            foreach (IRoster team in teams)
            {
                float totalPoints = 0;
                ListViewItem item = new ListViewItem(team.TeamName);
                item.Tag = team;
                item.SubItems.Add(string.Empty); // total points
                foreach (IStatExtractor extractor in lc.ScoringStatExtractors)
                {
                    item.SubItems.Add(team.Points[extractor.StatName].ToString());
                    totalPoints += team.Points[extractor.StatName];
                }
                item.SubItems[1].Text = totalPoints.ToString();
                lv.Items.Add(item);
            }
            lv.EndUpdate();
        }

        private static void OnColumnClicked(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
        }
    }
}
