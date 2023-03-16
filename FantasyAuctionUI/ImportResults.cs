using FantasyAlgorithms.DataModel;
using System;
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
    public struct ImportResult
    {
        public string Player;
        public string MLBTeam;
        public List<string> Positions;
        public string FantasyTeam;
        public string Cost;
        public string Error;
    }

    public partial class ImportResults : Form
    {
        public ImportResults(List<ImportResult> results)
        {
            InitializeComponent();

            results.Sort((a, b) => a.Error.CompareTo(b.Error));

            foreach (ImportResult result in results)
            {
                ListViewItem item = new ListViewItem(result.Player);
                item.SubItems.Add(result.MLBTeam);
                item.SubItems.Add(string.Join(", ", result.Positions));
                item.SubItems.Add(result.FantasyTeam);
                item.SubItems.Add(result.Cost);
                item.SubItems.Add(result.Error);
                this.lvResults.Items.Add(item);
            }
        }
    }
}
