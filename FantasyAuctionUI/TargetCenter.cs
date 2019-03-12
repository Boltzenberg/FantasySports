using FantasyAlgorithms;
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

            foreach (Team team in this.league.Teams)
            {
                this.cbTeams.Items.Add(team.Name);
            }

            this.lvPlayers.BeginUpdate();
            int columnWidth = this.lvPlayers.Width / (League.ScoringStatExtractors.Count + 2);
            this.lvPlayers.Columns.Add("Player Name", columnWidth);
            foreach (IStatExtractor extractor in League.ScoringStatExtractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                this.lvPlayers.Columns.Add(column);
            }
            this.lvPlayers.Columns.Add("Stat Delta");
            this.lvPlayers.EndUpdate();
        }

        private void OnTeamSelected(object sender, EventArgs e)
        {
            LeagueAnalysis baselineLA = LeagueAnalysis.Analyze(this.league, League.ScoringStatExtractors, p => p.FantasyTeam);
            TeamAnalysis baselineTA = baselineLA.Teams.Find(t => t.Team.Name == this.cbTeams.Items[this.cbTeams.SelectedIndex].ToString());
            if (baselineTA == null)
            {
                return;
            }

            this.lvPlayers.BeginUpdate();
            this.lvPlayers.Items.Clear();
            League l = this.league.Clone();
            foreach (IPlayer p in l.AllPlayers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam))
                {
                    continue;
                }

                p.FantasyTeam = baselineTA.Team.Name;

                LeagueAnalysis la = LeagueAnalysis.Analyze(l, League.ScoringStatExtractors, pb => pb.FantasyTeam);
                TeamAnalysis ta = la.Teams.Find(t => t.Team.Name == baselineTA.Team.Name);

                ListViewItem item = new ListViewItem(p.Name);
                float totalDelta = 0f;
                foreach (IStatExtractor extractor in League.ScoringStatExtractors)
                {
                    float delta = ta.Points[extractor.StatName] - baselineTA.Points[extractor.StatName];
                    totalDelta += delta;
                    item.SubItems.Add(delta.ToString());
                }
                item.SubItems.Add(totalDelta.ToString());
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
    }
}
