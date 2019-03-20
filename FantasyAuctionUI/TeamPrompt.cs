using FantasyAlgorithms.DataModel;
using System;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class TeamPrompt : Form
    {
        public string SelectedTeam { get; private set; }

        public TeamPrompt(League league)
        {
            InitializeComponent();

            this.cbTeams.BeginUpdate();
            this.cbTeams.Items.Clear();
            foreach (Team team in league.Teams)
            {
                this.cbTeams.Items.Add(team.Name);
            }
            this.cbTeams.EndUpdate();
        }

        private void OnOK(object sender, EventArgs e)
        {
            this.SelectedTeam = this.cbTeams.Items[this.cbTeams.SelectedIndex].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
