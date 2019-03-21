using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    public partial class PromptFromList : Form
    {
        public string SelectedItem { get; private set; }

        public PromptFromList(string title, IEnumerable<string> items)
        {
            InitializeComponent();
            this.Text = title;

            this.cbItems.BeginUpdate();
            this.cbItems.Items.Clear();
            foreach (string item in items)
            {
                this.cbItems.Items.Add(item);
            }
            this.cbItems.EndUpdate();
        }

        private void OnOK(object sender, EventArgs e)
        {
            this.SelectedItem = this.cbItems.Items[this.cbItems.SelectedIndex].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.SelectedItem = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
