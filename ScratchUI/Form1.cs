using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScratchUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await YahooFantasySports.AuthManager.Instance.AuthorizeAsync(this.tbAuthCode.Text.Trim());
            MessageBox.Show(await YahooFantasySports.AuthManager.Instance.GetAuthTokenAsync());
            MessageBox.Show(await YahooFantasySports.AuthManager.Instance.GetAuthTokenAsync());
        }

        private void OnLoad(object sender, EventArgs e)
        {
            YahooFantasySports.SignInDialog dlg = new YahooFantasySports.SignInDialog();
            dlg.ShowDialog();
        }
    }
}
