using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YahooFantasySports
{
    public partial class SignInDialog : Form
    {
        public SignInDialog()
        {
            InitializeComponent();
            this.wb.Url = AuthManager.GetAuthUrl();
        }
    }
}
