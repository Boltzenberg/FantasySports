﻿using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class PlayerAssignment : Form
    {
        private League league;
        private List<IPlayer> allPlayers;
        private IPlayer currentPlayer;
        private string yahooAuthToken = string.Empty;

        public PlayerAssignment()
        {
            InitializeComponent();
            this.tbDir.Text = "C:\\users\\jonro\\documents";
            this.LoadPlayers(Path.Combine(this.tbDir.Text, Constants.Files.League));
        }

        private void WriteUIToCurrentPlayer()
        {
            if (this.currentPlayer != null)
            {
                if (string.IsNullOrEmpty(this.tbFantasyTeam.Text) || Array.Exists(this.league.Teams, x => x.Name == this.tbFantasyTeam.Text))
                {
                    this.currentPlayer.FantasyTeam = this.tbFantasyTeam.Text;
                }

                if (string.IsNullOrEmpty(this.currentPlayer.FantasyTeam) && 
                    (string.IsNullOrEmpty(this.tbAssumedFantasyTeam.Text) || Array.Exists(this.league.Teams, x => x.Name == this.tbAssumedFantasyTeam.Text)))
                {
                    this.currentPlayer.AssumedFantasyTeam = this.tbAssumedFantasyTeam.Text;
                }

                float price;
                if (float.TryParse(this.tbAuctionPrice.Text, out price))
                {
                    this.currentPlayer.AuctionPrice = price;
                }
                else if (string.IsNullOrEmpty(this.tbAuctionPrice.Text))
                {
                    this.currentPlayer.AuctionPrice = 0.0f;
                }
            }
        }

        private void UpdateCurrentPlayer(IPlayer newCurrentPlayer)
        {
            this.WriteUIToCurrentPlayer();
            if (newCurrentPlayer != null)
            {
                this.tbAuctionPrice.Text = newCurrentPlayer.AuctionPrice.ToString();
                this.tbFantasyTeam.Text = newCurrentPlayer.FantasyTeam;
                this.tbAssumedFantasyTeam.Text = newCurrentPlayer.AssumedFantasyTeam;
                this.wbOut.DocumentText = newCurrentPlayer.GetHTML();
            }
            this.currentPlayer = newCurrentPlayer;
        }

        private void OnSelectPlayer(object sender, EventArgs e)
        {
            UpdateCurrentPlayer(this.lbPlayers.SelectedItem as IPlayer);
        }

        private void OnLoadData(object sender, EventArgs e)
        {
            this.LoadPlayers(Path.Combine(this.tbDir.Text, Constants.Files.League));
        }

        private void LoadPlayers(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            this.league = League.Load(fileName);
            this.allPlayers = new List<IPlayer>();
            this.allPlayers.AddRange(this.league.AllPlayers);
            this.allPlayers.Sort((x, y) => x.Name.CompareTo(y.Name));
            this.tbWordWheel.Text = string.Empty;
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.ToArray());
            this.currentPlayer = null;
        }

        private void OnSave(object sender, EventArgs e)
        {
            this.WriteUIToCurrentPlayer();
            this.league.Save(Path.Combine(this.tbDir.Text, Constants.Files.League));
        }

        private void OnReloadESPNData(object sender, EventArgs e)
        {
            this.league.UpdateESPNProjections();
        }

        private async void OnReloadYahooData(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.yahooAuthToken))
            {
                System.Diagnostics.Process.Start(YahooFantasySports.Services.AuthManager.GetAuthUrl().AbsoluteUri);
                Prompt prompt = new Prompt("Enter Auth Code:");
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    await YahooFantasySports.Services.AuthManager.Instance.InitializeAuthorizationAsync(prompt.Answer);
                    this.yahooAuthToken = prompt.Answer;
                }
            }

            if (!string.IsNullOrEmpty(this.yahooAuthToken))
            {
                await this.league.UpdateYahooData();
            }
        }

        private void OnLaunchStatCenter(object sender, EventArgs e)
        {
            new StatCenter(this.league.Clone(), p => p.FantasyTeam).Show();
        }

        private void OnLaunchRosterCenter(object sender, EventArgs e)
        {
            new RosterCenter(this.league.Clone()).Show();
        }

        private void OnLaunchAnalysisCenter(object sender, EventArgs e)
        {
            TeamPrompt prompt = new TeamPrompt(this.league);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                new AnalysisCenter(this.league.Clone(), this.league.Teams.First(t => t.Name == prompt.SelectedTeam)).Show();
            }
        }

        private void OnLaunchSpeculationCenter(object sender, EventArgs e)
        {
            new StatCenter(this.league.Clone(), p => string.IsNullOrEmpty(p.FantasyTeam) ? p.AssumedFantasyTeam : p.FantasyTeam).Show();
        }

        private void OnLaunchTargetCenter(object sender, EventArgs e)
        {
            new TargetCenter(this.league.Clone()).Show();
        }

        private void OnWordWheel(object sender, EventArgs e)
        {
            this.lbPlayers.BeginUpdate();
            this.lbPlayers.Items.Clear();
            this.lbPlayers.Items.AddRange(this.allPlayers.Where(x => x.Name.StartsWith(this.tbWordWheel.Text, StringComparison.OrdinalIgnoreCase)).ToArray());
            this.lbPlayers.EndUpdate();
        }

        private void OnGetTopFreeAgentSwaps(object sender, EventArgs e)
        {
            TeamPrompt prompt = new TeamPrompt(this.league);
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string html = FantasyAlgorithms.RosterAnalysis.GetTopNFreeAgentPickups(this.league.Clone(), League.ScoringStatExtractors, prompt.SelectedTeam);
                this.wbOut.DocumentText = html;
            }
        }
    }
}
