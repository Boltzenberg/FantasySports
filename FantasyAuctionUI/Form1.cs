using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class Form1 : Form
    {
        private List<FantasyAuction.DataModel.Batter> batters;
        private List<FantasyAuction.DataModel.Pitcher> pitchers;
        private FantasyAuction.DataModel.IPlayer currentPlayer;

        public Form1()
        {
            InitializeComponent();
            this.tbDir.Text = "C:\\users\\jonro\\documents";
        }

        private void UpdateCurrentPlayer(FantasyAuction.DataModel.IPlayer newCurrentPlayer)
        {
            if (this.currentPlayer != null)
            {
                this.currentPlayer.FantasyTeam = this.tbFantasyTeam.Text;
                float price;
                if (float.TryParse(this.tbAuctionPrice.Text, out price))
                {
                    this.currentPlayer.AuctionPrice = price;
                }
            }

            this.tbAuctionPrice.Text = newCurrentPlayer.AuctionPrice.ToString();
            this.tbFantasyTeam.Text = newCurrentPlayer.FantasyTeam;
            this.wbOut.DocumentText = newCurrentPlayer.GetHTML();
            this.currentPlayer = newCurrentPlayer;
        }

        private void OnSelectBatter(object sender, EventArgs e)
        {
            UpdateCurrentPlayer(this.lbBatters.SelectedItem as FantasyAuction.DataModel.Batter);
        }

        private void OnSelectPitcher(object sender, EventArgs e)
        {
            UpdateCurrentPlayer(this.lbPitchers.SelectedItem as FantasyAuction.DataModel.Pitcher);
        }

        private void OnLoadData(object sender, EventArgs e)
        {
            string batterFile = Path.Combine(this.tbDir.Text, Constants.Files.Batters);
            string pitcherFile = Path.Combine(this.tbDir.Text, Constants.Files.Pitchers);
            if (!File.Exists(batterFile) || !File.Exists(pitcherFile))
            {
                FantasyAuction.Year.Create(batterFile, pitcherFile);
            }

            this.batters = new List<FantasyAuction.DataModel.Batter>(FantasyAuction.DataModel.Batter.Load(File.ReadAllText(batterFile)));
            this.pitchers = new List<FantasyAuction.DataModel.Pitcher>(FantasyAuction.DataModel.Pitcher.Load(File.ReadAllText(pitcherFile)));

            this.batters.Sort((x, y) => x.Name.CompareTo(y.Name));
            this.pitchers.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (var batter in this.batters)
            {
                this.lbBatters.Items.Add(batter);
            }

            foreach (var pitcher in this.pitchers)
            {
                this.lbPitchers.Items.Add(pitcher);
            }

            this.currentPlayer = null;
        }

        private void OnSave(object sender, EventArgs e)
        {
            string batterFile = Path.Combine(this.tbDir.Text, Constants.Files.Batters);
            string pitcherFile = Path.Combine(this.tbDir.Text, Constants.Files.Pitchers);

            File.WriteAllText(batterFile, FantasyAuction.DataModel.Batter.Serialize(this.batters.ToArray()));
            File.WriteAllText(pitcherFile, FantasyAuction.DataModel.Pitcher.Serialize(this.pitchers.ToArray()));
        }
    }
}
