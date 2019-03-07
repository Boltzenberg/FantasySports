using FantasyAlgorithms;
using FantasyAlgorithms.DataModel;
using System.Linq;
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
            int columnWidth = lv.Width / (League.StatExtractors.Count + 1);
            lv.Columns.Add("Player Name", columnWidth);
            foreach (IStatExtractor extractor in League.StatExtractors)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = extractor.StatName;
                column.Width = columnWidth;
                lv.Columns.Add(column);
            }

            foreach (IPlayer player in team.AllPlayers)
            {
                ListViewItem item = new ListViewItem(player.Name);
                foreach (IStatExtractor extractor in League.StatExtractors)
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
                this.lv.Items.Add(item);
            }

            ListViewItem total = new ListViewItem("Totals");
            foreach (IStatExtractor extractor in League.StatExtractors)
            {
                float value;
                if (team.Stats.TryGetValue(extractor.StatName, out value))
                {
                    total.SubItems.Add(value.ToString());
                }
                else
                {
                    total.SubItems.Add(string.Empty);
                }
            }
            this.lv.Items.Add(total);
            this.lv.EndUpdate();
        }
    }
}
