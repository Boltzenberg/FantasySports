using System;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class Prompt : Form
    {
        public string Answer { get; private set; }

        public Prompt(string question)
        {
            InitializeComponent();
            this.lblPrompt.Text = question;
        }

        private void OnOK(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Answer = this.tbText.Text;
            this.Close();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
