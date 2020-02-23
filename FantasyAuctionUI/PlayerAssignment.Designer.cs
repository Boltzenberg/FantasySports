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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAuctionPrice = new System.Windows.Forms.TextBox();
            this.wbOut = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.draftCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.draftCenterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rosterCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speculationCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.targetCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topFreeAgentSwapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snakeCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fantasyDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadYahooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaguesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbPlayers = new System.Windows.Forms.ListBox();
            this.tbWordWheel = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbFantasyTeam = new System.Windows.Forms.TextBox();
            this.tbAssumedFantasyTeam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.playerGroupAnalysisCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Players:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 992);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fantasy Team:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 1032);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Auction Price:";
            // 
            // tbAuctionPrice
            // 
            this.tbAuctionPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAuctionPrice.Location = new System.Drawing.Point(444, 1028);
            this.tbAuctionPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbAuctionPrice.Name = "tbAuctionPrice";
            this.tbAuctionPrice.Size = new System.Drawing.Size(556, 26);
            this.tbAuctionPrice.TabIndex = 2;
            // 
            // wbOut
            // 
            this.wbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbOut.Location = new System.Drawing.Point(255, 83);
            this.wbOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wbOut.MinimumSize = new System.Drawing.Size(30, 31);
            this.wbOut.Name = "wbOut";
            this.wbOut.Size = new System.Drawing.Size(927, 891);
            this.wbOut.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1011, 988);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 112);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnSave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draftCenterToolStripMenuItem,
            this.fantasyDataToolStripMenuItem,
            this.leaguesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 35);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // draftCenterToolStripMenuItem
            // 
            this.draftCenterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draftCenterToolStripMenuItem1,
            this.rosterCenterToolStripMenuItem,
            this.analysisCenterToolStripMenuItem,
            this.speculationCenterToolStripMenuItem,
            this.targetCenterToolStripMenuItem,
            this.topFreeAgentSwapsToolStripMenuItem,
            this.snakeCenterToolStripMenuItem,
            this.playerGroupAnalysisCenterToolStripMenuItem});
            this.draftCenterToolStripMenuItem.Name = "draftCenterToolStripMenuItem";
            this.draftCenterToolStripMenuItem.Size = new System.Drawing.Size(87, 29);
            this.draftCenterToolStripMenuItem.Text = "Centers";
            // 
            // draftCenterToolStripMenuItem1
            // 
            this.draftCenterToolStripMenuItem1.Name = "draftCenterToolStripMenuItem1";
            this.draftCenterToolStripMenuItem1.Size = new System.Drawing.Size(341, 34);
            this.draftCenterToolStripMenuItem1.Text = "Stat Center";
            this.draftCenterToolStripMenuItem1.Click += new System.EventHandler(this.OnLaunchStatCenter);
            // 
            // rosterCenterToolStripMenuItem
            // 
            this.rosterCenterToolStripMenuItem.Name = "rosterCenterToolStripMenuItem";
            this.rosterCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.rosterCenterToolStripMenuItem.Text = "Roster Center";
            this.rosterCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchRosterCenter);
            // 
            // analysisCenterToolStripMenuItem
            // 
            this.analysisCenterToolStripMenuItem.Name = "analysisCenterToolStripMenuItem";
            this.analysisCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.analysisCenterToolStripMenuItem.Text = "Analysis Center";
            this.analysisCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchAnalysisCenter);
            // 
            // speculationCenterToolStripMenuItem
            // 
            this.speculationCenterToolStripMenuItem.Name = "speculationCenterToolStripMenuItem";
            this.speculationCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.speculationCenterToolStripMenuItem.Text = "Speculation Center";
            this.speculationCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchSpeculationCenter);
            // 
            // targetCenterToolStripMenuItem
            // 
            this.targetCenterToolStripMenuItem.Name = "targetCenterToolStripMenuItem";
            this.targetCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.targetCenterToolStripMenuItem.Text = "Target Center";
            this.targetCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchTargetCenter);
            // 
            // topFreeAgentSwapsToolStripMenuItem
            // 
            this.topFreeAgentSwapsToolStripMenuItem.Name = "topFreeAgentSwapsToolStripMenuItem";
            this.topFreeAgentSwapsToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.topFreeAgentSwapsToolStripMenuItem.Text = "Top Free Agent Swaps";
            this.topFreeAgentSwapsToolStripMenuItem.Click += new System.EventHandler(this.OnGetTopFreeAgentSwaps);
            // 
            // snakeCenterToolStripMenuItem
            // 
            this.snakeCenterToolStripMenuItem.Name = "snakeCenterToolStripMenuItem";
            this.snakeCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.snakeCenterToolStripMenuItem.Text = "Snake Center";
            this.snakeCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchSnakeCenter);
            // 
            // fantasyDataToolStripMenuItem
            // 
            this.fantasyDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.reloadYahooToolStripMenuItem});
            this.fantasyDataToolStripMenuItem.Name = "fantasyDataToolStripMenuItem";
            this.fantasyDataToolStripMenuItem.Size = new System.Drawing.Size(129, 29);
            this.fantasyDataToolStripMenuItem.Text = "Fantasy Data";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.reloadToolStripMenuItem.Text = "Reload ESPN";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.OnReloadESPNData);
            // 
            // reloadYahooToolStripMenuItem
            // 
            this.reloadYahooToolStripMenuItem.Name = "reloadYahooToolStripMenuItem";
            this.reloadYahooToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.reloadYahooToolStripMenuItem.Text = "Reload Yahoo";
            this.reloadYahooToolStripMenuItem.Click += new System.EventHandler(this.OnReloadYahooData);
            // 
            // leaguesToolStripMenuItem
            // 
            this.leaguesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.createToolStripMenuItem});
            this.leaguesToolStripMenuItem.Name = "leaguesToolStripMenuItem";
            this.leaguesToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.leaguesToolStripMenuItem.Text = "Leagues";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.OnLoadLeague);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.createToolStripMenuItem.Text = "Create...";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.OnCreateNewLeague);
            // 
            // lbPlayers
            // 
            this.lbPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPlayers.FormattingEnabled = true;
            this.lbPlayers.ItemHeight = 20;
            this.lbPlayers.Location = new System.Drawing.Point(20, 118);
            this.lbPlayers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(224, 984);
            this.lbPlayers.TabIndex = 0;
            this.lbPlayers.SelectedIndexChanged += new System.EventHandler(this.OnSelectPlayer);
            // 
            // tbWordWheel
            // 
            this.tbWordWheel.Location = new System.Drawing.Point(20, 83);
            this.tbWordWheel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbWordWheel.Name = "tbWordWheel";
            this.tbWordWheel.Size = new System.Drawing.Size(224, 26);
            this.tbWordWheel.TabIndex = 15;
            this.tbWordWheel.TextChanged += new System.EventHandler(this.OnWordWheel);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(888, 1917);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 28);
            this.comboBox1.TabIndex = 16;
            // 
            // tbFantasyTeam
            // 
            this.tbFantasyTeam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFantasyTeam.Location = new System.Drawing.Point(444, 988);
            this.tbFantasyTeam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbFantasyTeam.Name = "tbFantasyTeam";
            this.tbFantasyTeam.Size = new System.Drawing.Size(556, 26);
            this.tbFantasyTeam.TabIndex = 1;
            // 
            // tbAssumedFantasyTeam
            // 
            this.tbAssumedFantasyTeam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAssumedFantasyTeam.Location = new System.Drawing.Point(444, 1069);
            this.tbAssumedFantasyTeam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbAssumedFantasyTeam.Name = "tbAssumedFantasyTeam";
            this.tbAssumedFantasyTeam.Size = new System.Drawing.Size(556, 26);
            this.tbAssumedFantasyTeam.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 1074);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Assumed Fantasy Team:";
            // 
            // playerGroupAnalysisCenterToolStripMenuItem
            // 
            this.playerGroupAnalysisCenterToolStripMenuItem.Name = "playerGroupAnalysisCenterToolStripMenuItem";
            this.playerGroupAnalysisCenterToolStripMenuItem.Size = new System.Drawing.Size(341, 34);
            this.playerGroupAnalysisCenterToolStripMenuItem.Text = "Player Group Analysis Center";
            this.playerGroupAnalysisCenterToolStripMenuItem.Click += new System.EventHandler(this.OnLaunchPlayerGroupAnalysisCenter);
            // 
            // PlayerAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 1123);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbAssumedFantasyTeam);
            this.Controls.Add(this.tbFantasyTeam);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tbWordWheel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wbOut);
            this.Controls.Add(this.tbAuctionPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPlayers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PlayerAssignment";
            this.Text = "Fantasy Auction UI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAuctionPrice;
        private System.Windows.Forms.WebBrowser wbOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem draftCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fantasyDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ListBox lbPlayers;
        private System.Windows.Forms.TextBox tbWordWheel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem draftCenterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rosterCenterToolStripMenuItem;
        private System.Windows.Forms.TextBox tbFantasyTeam;
        private System.Windows.Forms.ToolStripMenuItem analysisCenterToolStripMenuItem;
        private System.Windows.Forms.TextBox tbAssumedFantasyTeam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem speculationCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem targetCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadYahooToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topFreeAgentSwapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaguesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snakeCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerGroupAnalysisCenterToolStripMenuItem;
    }
}

