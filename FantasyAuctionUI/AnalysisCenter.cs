using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class AnalysisCenter : Form
    {
        private League league;
        private Team team;
        private List<PlayerAggregate> allAggregates;
        private List<PlayerAggregate> filteredAggregates;
        private List<Stat> stats;

        public AnalysisCenter(League league, Team team)
        {
            InitializeComponent();

            this.league = league;
            this.team = team;
            this.lblTeam.Text = string.Format("{0}: {1}, {2,12:C2}", team.Name, team.Owner, team.Budget);

            LeagueConstants lc = LeagueConstants.For(league.FantasyLeague);
            this.stats = new List<Stat>();
            this.stats.Add(new Stat("Runs", new CountingStatAnalyzer("Runs", lc.RosterableBatterCountPerTeam * lc.TeamCount, league.Batters, b => ((Batter)b).ProjectedR, b => string.IsNullOrEmpty(b.FantasyTeam) ? 0 : ((Batter)b).ProjectedR)));
            this.stats.Add(new Stat("Home Runs", new CountingStatAnalyzer("Home Runs", lc.RosterableBatterCountPerTeam * lc.TeamCount, league.Batters, b => ((Batter)b).ProjectedHR, b => string.IsNullOrEmpty(b.FantasyTeam) ? 0 : ((Batter)b).ProjectedHR)));
            this.stats.Add(new Stat("RBIs", new CountingStatAnalyzer("RBIs", lc.RosterableBatterCountPerTeam * lc.TeamCount, league.Batters, b => ((Batter)b).ProjectedRBI, b => string.IsNullOrEmpty(b.FantasyTeam) ? 0 : ((Batter)b).ProjectedRBI)));
            this.stats.Add(new Stat("Steals", new CountingStatAnalyzer("Steals", lc.RosterableBatterCountPerTeam * lc.TeamCount, league.Batters, b => ((Batter)b).ProjectedSB, b => string.IsNullOrEmpty(b.FantasyTeam) ? 0 : ((Batter)b).ProjectedSB)));
            this.stats.Add(new Stat("OPS", new RatioStatAnalyzer("OPS", lc.RosterableBatterCountPerTeam * lc.TeamCount, true, league.Batters, b => ((Batter)b).ProjectedWalksPlusHits, b => ((Batter)b).ProjectedAB)));
            this.stats.Add(new Stat("Wins", new CountingStatAnalyzer("Wins", lc.RosterablePitcherCountPerTeam * lc.TeamCount, league.Pitchers, p => ((Pitcher)p).ProjectedW, p => string.IsNullOrEmpty(p.FantasyTeam) ? 0 : ((Pitcher)p).ProjectedW)));
            this.stats.Add(new Stat("Saves", new CountingStatAnalyzer("Saves", lc.RosterablePitcherCountPerTeam * lc.TeamCount, league.Pitchers, p => ((Pitcher)p).ProjectedSV, p => string.IsNullOrEmpty(p.FantasyTeam) ? 0 : ((Pitcher)p).ProjectedSV)));
            this.stats.Add(new Stat("Strikeouts", new CountingStatAnalyzer("Strikeouts", lc.RosterablePitcherCountPerTeam * lc.TeamCount, league.Pitchers, p => ((Pitcher)p).ProjectedK, p => string.IsNullOrEmpty(p.FantasyTeam) ? 0 : ((Pitcher)p).ProjectedK)));
            this.stats.Add(new Stat("ERA", new RatioStatAnalyzer("ERA", lc.RosterablePitcherCountPerTeam * lc.TeamCount, false, league.Pitchers, p => ((Pitcher)p).ProjectedER * 27, p => ((Pitcher)p).ProjectedOutsRecorded)));
            this.stats.Add(new Stat("WHIP", new RatioStatAnalyzer("WHIP", lc.RosterablePitcherCountPerTeam * lc.TeamCount, false, league.Pitchers, p => (((Pitcher)p).ProjectedWalks + ((Pitcher)p).ProjectedHits) * 3, p => ((Pitcher)p).ProjectedOutsRecorded)));

            Dictionary<IPlayer, PlayerAggregate> playerAnalysis = new Dictionary<IPlayer, PlayerAggregate>();
            foreach (Stat stat in this.stats)
            {
                foreach (PlayerAnalysis analysis in stat.Analyzer.Analyze())
                {
                    PlayerAggregate agg;
                    if (!playerAnalysis.TryGetValue(analysis.Player, out agg))
                    {
                        agg = new PlayerAggregate(analysis.Player);
                        playerAnalysis[analysis.Player] = agg;
                    }
                    agg.Analyses.Add(analysis);
                }
            }

            float budgetPerStat = team.Budget / this.stats.Count;
            this.allAggregates = new List<PlayerAggregate>();
            foreach (PlayerAggregate agg in playerAnalysis.Values)
            {
                agg.Summarize(budgetPerStat, this.stats);
                this.allAggregates.Add(agg);
            }

            this.allAggregates.Sort((x, y) => y.ProjectedValue.CompareTo(x.ProjectedValue));
            this.filteredAggregates = new List<PlayerAggregate>();
            this.UpdatePlayerFilter();
            this.WordWheelPlayers();
            this.UpdateWB();

            this.lvAnalysis.BeginUpdate();
            int columnWidth = this.lvAnalysis.Width / (lc.ScoringStatExtractors.Count + lc.SupportingStatExtractors.Count + 1);
            this.lvAnalysis.Columns.Add("Player Name", columnWidth);
            foreach (IStatExtractor extractor in lc.ScoringStatExtractors.Union(lc.SupportingStatExtractors))
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                if (!extractor.MoreIsBetter)
                {
                    column.Tag = "asc";
                }

                this.lvAnalysis.Columns.Add(column);
            }
            this.lvAnalysis.EndUpdate();

            this.UpdatePlayerListView(league.AllPlayers);
        }

        private void UpdatePlayerListView(IEnumerable<IPlayer> players)
        {
            this.lvAnalysis.BeginUpdate();
            this.lvAnalysis.Items.Clear();

            foreach (IPlayer player in players)
            {
                ListViewItem item = new ListViewItem(player.Name);
                foreach (IStatExtractor extractor in LeagueConstants.For(this.league.FantasyLeague).ScoringStatExtractors.Union(LeagueConstants.For(this.league.FantasyLeague).SupportingStatExtractors))
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
                this.lvAnalysis.Items.Add(item);
            }

            this.lvAnalysis.EndUpdate();
        }

        private void UpdateWB()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1'>");
            sb.AppendLine("<TR><TD>Name</TD>");
            foreach (Stat stat in this.stats)
            {
                sb.AppendFormat("<TD>{0}</TD>", stat.Name);
            }
            sb.AppendLine("<TD>Projected Value</TD></TR>");

            foreach (PlayerAggregate agg in this.filteredAggregates)
            {
                sb.AppendLine(agg.HTML);
            }

            sb.AppendLine("</TABLE></BODY></HTML>");
            this.wb.DocumentText = sb.ToString();
        }

        private void OnWordWheel(object sender, EventArgs e)
        {
            this.WordWheelPlayers();
        }

        private void WordWheelPlayers()
        {
            this.lbPlayers.BeginUpdate();
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.filteredAggregates.Where(x => x.Player.Name.StartsWith(this.tbWordWheel.Text, StringComparison.OrdinalIgnoreCase)).ToArray());
            this.lbPlayers.EndUpdate();
        }

        private void OnUpdatePlayerFilter(object sender, EventArgs e)
        {
            UpdatePlayerFilter();
        }

        private bool PlayerPassesFilters(IPlayer player)
        {
            if (cbUndrafted.Checked && !string.IsNullOrEmpty(player.FantasyTeam)) return false;

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

        private void UpdatePlayerFilter()
        { 
            this.filteredAggregates.Clear();
            foreach (var agg in this.allAggregates)
            {
                if (this.PlayerPassesFilters(agg.Player))
                {
                    this.filteredAggregates.Add(agg);
                }
            }

            this.WordWheelPlayers();
            this.UpdateWB();
            this.UpdatePlayerListView(this.filteredAggregates.Select(p => p.Player));
        }

        private class PlayerAggregate
        {
            public IPlayer Player { get; private set; }
            public List<PlayerAnalysis> Analyses { get; private set; }
            public float ProjectedValue { get; private set; }
            public string HTML { get; private set; }

            public PlayerAggregate(IPlayer player)
            {
                this.Player = player;
                this.Analyses = new List<PlayerAnalysis>();
                this.ProjectedValue = 0f;
                this.HTML = string.Empty;
            }

            public void Summarize(float budgetPerStat, List<Stat> stats)
            {
                this.ProjectedValue = 0f;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<TR><TD>{0}</TD>", this.Player.Name);
                foreach (Stat stat in stats)
                {
                    PlayerAnalysis a = this.Analyses.FirstOrDefault(pa => pa.Stat == stat.Name);
                    if (a != null)
                    {
                        float statValue = budgetPerStat * a.Percentage;
                        sb.AppendFormat("<TD>Raw Value: {0}<BR>Rank: {1}<BR>Percentage: {2}<BR>Value: {3}</TD>", a.RawValue, a.Rank, a.Percentage, statValue);
                        this.ProjectedValue += statValue;
                    }
                    else
                    {
                        sb.Append("<TD>&nbsp;</TD>");
                    }
                }
                sb.AppendFormat("<TD>{0}</TD>", this.ProjectedValue);
                sb.AppendLine("</TR>");
                this.HTML = sb.ToString();
            }

            public override string ToString()
            {
                return string.Format("{0}: {1}", this.Player.Name, this.ProjectedValue);
            }
        }

        private class Stat
        {
            public string Name { get; private set; }
            public IPlayerAnalyzer Analyzer { get; private set; }

            public Stat(string name, IPlayerAnalyzer analyzer)
            {
                this.Name = name;
                this.Analyzer = analyzer;
            }
        }

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            object tag = lv.Columns[e.Column].Tag;
            lv.ListViewItemSorter = new StatColumnSorter(e.Column, tag == null || tag.ToString() != "asc");
        }
    }
}
