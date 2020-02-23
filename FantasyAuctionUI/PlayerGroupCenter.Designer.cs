namespace FantasyAuctionUI
{
    partial class PlayerGroupCenter
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
            this.tbWordWheel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPlayers = new System.Windows.Forms.ListBox();
            this.wbOut = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // tbWordWheel
            // 
            this.tbWordWheel.Location = new System.Drawing.Point(17, 38);
            this.tbWordWheel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbWordWheel.Name = "tbWordWheel";
            this.tbWordWheel.Size = new System.Drawing.Size(224, 26);
            this.tbWordWheel.TabIndex = 18;
            this.tbWordWheel.TextChanged += new System.EventHandler(this.OnWordWheel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Players:";
            // 
            // lbPlayers
            // 
            this.lbPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPlayers.FormattingEnabled = true;
            this.lbPlayers.ItemHeight = 20;
            this.lbPlayers.Location = new System.Drawing.Point(17, 73);
            this.lbPlayers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(224, 1144);
            this.lbPlayers.TabIndex = 16;
            this.lbPlayers.SelectedIndexChanged += new System.EventHandler(this.OnPlayerSelected);
            // 
            // wbOut
            // 
            this.wbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbOut.Location = new System.Drawing.Point(249, 38);
            this.wbOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wbOut.MinimumSize = new System.Drawing.Size(30, 31);
            this.wbOut.Name = "wbOut";
            this.wbOut.Size = new System.Drawing.Size(1935, 1179);
            this.wbOut.TabIndex = 19;
            // 
            // PlayerGroupCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2197, 1362);
            this.Controls.Add(this.wbOut);
            this.Controls.Add(this.tbWordWheel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPlayers);
            this.Name = "PlayerGroupCenter";
            this.Text = "PlayerGroupCenter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWordWheel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbPlayers;
        private System.Windows.Forms.WebBrowser wbOut;
    }
}