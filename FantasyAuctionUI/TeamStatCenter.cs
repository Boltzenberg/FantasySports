using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class TeamStatCenter : Form
    {
        public TeamStatCenter(TeamAnalysis team)
        {
            InitializeComponent();
            this.Text = string.Format("Team Stat Center: {0}", team.Team.Name);

            this.lv.BeginUpdate();
            int columnWidth = lv.Width / (League.ScoringStatExtractors.Count + 2);
            lv.Columns.Add("Player Name", columnWidth);
            lv.Columns.Add("Player Status", columnWidth);
            foreach (IStatExtractor extractor in League.ScoringStatExtractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                lv.Columns.Add(column);
            }

            Dictionary<string, int> statToPlayerCount = new Dictionary<string, int>();
            foreach (IPlayer player in team.Players)
            {
                ListViewItem item = new ListViewItem(player.Name);
                item.SubItems.Add(player.Status);
                foreach (IStatExtractor extractor in League.ScoringStatExtractors)
                {
                    IStatValue value = extractor.Extract(player);
                    if (value != null)
                    {
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
                this.lv.Items.Add(item);
            }

            ListViewItem total = new ListViewItem("Totals");
            ListViewItem average = new ListViewItem("Average");
            total.SubItems.Add(string.Empty); // player status
            average.SubItems.Add(string.Empty); // player status
            foreach (IStatExtractor extractor in League.ScoringStatExtractors)
            {
                float value;
                if (team.Stats.TryGetValue(extractor.StatName, out value))
                {
                    total.SubItems.Add(value.ToString());
                    int count;
                    if (statToPlayerCount.TryGetValue(extractor.StatName, out count))
                    {
                        average.SubItems.Add((value / (float)count).ToString());
                    }
                    else
                    {
                        average.SubItems.Add(string.Empty);
                    }
                }
                else
                {
                    total.SubItems.Add(string.Empty);
                    average.SubItems.Add(string.Empty);
                }
            }
            this.lv.Items.Add(total);
            this.lv.Items.Add(average);
            this.lv.EndUpdate();
        }
    }
}
