namespace FantasyAuctionUI
{
    partial class PlayerMatcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbYahooPlayers = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.lbEspn = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbYahooPlayers
            // 
            this.lbYahooPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbYahooPlayers.FormattingEnabled = true;
            this.lbYahooPlayers.ItemHeight = 24;
            this.lbYahooPlayers.Location = new System.Drawing.Point(1680, 16);
            this.lbYahooPlayers.Name = "lbYahooPlayers";
            this.lbYahooPlayers.Size = new System.Drawing.Size(600, 820);
            this.lbYahooPlayers.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(25, 843);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnSave);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1437, 843);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(237, 41);
            this.button2.TabIndex = 4;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnCancel);
            // 
            // wb
            // 
            this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb.Location = new System.Drawing.Point(278, 16);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(1396, 820);
            this.wb.TabIndex = 5;
            // 
            // lbEspn
            // 
            this.lbEspn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbEspn.FormattingEnabled = true;
            this.lbEspn.ItemHeight = 24;
            this.lbEspn.Location = new System.Drawing.Point(12, 12);
            this.lbEspn.Name = "lbEspn";
            this.lbEspn.Size = new System.Drawing.Size(260, 820);
            this.lbEspn.TabIndex = 6;
            this.lbEspn.SelectedIndexChanged += new System.EventHandler(this.OnESPNPlayerSelected);
            this.lbEspn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            // 
            // PlayerMatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2292, 896);
            this.Controls.Add(this.lbEspn);
            this.Controls.Add(this.wb);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbYahooPlayers);
            this.Name = "PlayerMatcher";
            this.Text = "Player Matcher";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbYahooPlayers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.ListBox lbEspn;
    }
}