namespace FantasyAuctionUI
{
    partial class PlayerAssignment
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
            this.lbBatters = new System.Windows.Forms.ListBox();
            this.lbPitchers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFantasyTeam = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAuctionPrice = new System.Windows.Forms.TextBox();
            this.wbOut = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.tbDir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.draftCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fantasyDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbBatters
            // 
            this.lbBatters.FormattingEnabled = true;
            this.lbBatters.Location = new System.Drawing.Point(13, 51);
            this.lbBatters.Name = "lbBatters";
            this.lbBatters.Size = new System.Drawing.Size(151, 290);
            this.lbBatters.TabIndex = 0;
            this.lbBatters.SelectedIndexChanged += new System.EventHandler(this.OnSelectBatter);
            // 
            // lbPitchers
            // 
            this.lbPitchers.FormattingEnabled = true;
            this.lbPitchers.Location = new System.Drawing.Point(13, 378);
            this.lbPitchers.Name = "lbPitchers";
            this.lbPitchers.Size = new System.Drawing.Size(151, 290);
            this.lbPitchers.TabIndex = 1;
            this.lbPitchers.SelectedIndexChanged += new System.EventHandler(this.OnSelectPitcher);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Batters:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 362);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pitchers:";
            // 
            // tbFantasyTeam
            // 
            this.tbFantasyTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFantasyTeam.Location = new System.Drawing.Point(253, 622);
            this.tbFantasyTeam.Name = "tbFantasyTeam";
            this.tbFantasyTeam.Size = new System.Drawing.Size(415, 20);
            this.tbFantasyTeam.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 625);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fantasy Team:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 651);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Auction Price:";
            // 
            // tbAuctionPrice
            // 
            this.tbAuctionPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAuctionPrice.Location = new System.Drawing.Point(253, 648);
            this.tbAuctionPrice.Name = "tbAuctionPrice";
            this.tbAuctionPrice.Size = new System.Drawing.Size(415, 20);
            this.tbAuctionPrice.TabIndex = 8;
            // 
            // wbOut
            // 
            this.wbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbOut.Location = new System.Drawing.Point(170, 77);
            this.wbOut.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbOut.Name = "wbOut";
            this.wbOut.Size = new System.Drawing.Size(618, 539);
            this.wbOut.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(674, 622);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 46);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnSave);
            // 
            // tbDir
            // 
            this.tbDir.Location = new System.Drawing.Point(253, 51);
            this.tbDir.Name = "tbDir";
            this.tbDir.Size = new System.Drawing.Size(415, 20);
            this.tbDir.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Directory:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(674, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 20);
            this.button2.TabIndex = 13;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnLoadData);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draftCenterToolStripMenuItem,
            this.fantasyDataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // draftCenterToolStripMenuItem
            // 
            this.draftCenterToolStripMenuItem.Name = "draftCenterToolStripMenuItem";
            this.draftCenterToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.draftCenterToolStripMenuItem.Text = "Draft Center";
            this.draftCenterToolStripMenuItem.Click += new System.EventHandler(this.draftCenterToolStripMenuItem_Click);
            // 
            // fantasyDataToolStripMenuItem
            // 
            this.fantasyDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.fantasyDataToolStripMenuItem.Name = "fantasyDataToolStripMenuItem";
            this.fantasyDataToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.fantasyDataToolStripMenuItem.Text = "Fantasy Data";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // PlayerAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 677);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wbOut);
            this.Controls.Add(this.tbAuctionPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFantasyTeam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPitchers);
            this.Controls.Add(this.lbBatters);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PlayerAssignment";
            this.Text = "Fantasy Auction UI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbBatters;
        private System.Windows.Forms.ListBox lbPitchers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFantasyTeam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAuctionPrice;
        private System.Windows.Forms.WebBrowser wbOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem draftCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fantasyDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
    }
}

