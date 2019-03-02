namespace FantasyAuctionUI
{
    partial class StatCenter
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
            this.lvPoints = new System.Windows.Forms.ListView();
            this.chTeam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRuns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHomeRuns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRBI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSteals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOBP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWins = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSaves = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStrikeouts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chERA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chWHIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvStats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvPoints
            // 
            this.lvPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTeam,
            this.chRuns,
            this.chHomeRuns,
            this.chRBI,
            this.chSteals,
            this.chOBP,
            this.chWins,
            this.chSaves,
            this.chStrikeouts,
            this.chERA,
            this.chWHIP,
            this.chTotal});
            this.lvPoints.GridLines = true;
            this.lvPoints.Location = new System.Drawing.Point(12, 352);
            this.lvPoints.MultiSelect = false;
            this.lvPoints.Name = "lvPoints";
            this.lvPoints.Size = new System.Drawing.Size(826, 281);
            this.lvPoints.TabIndex = 1;
            this.lvPoints.UseCompatibleStateImageBehavior = false;
            this.lvPoints.View = System.Windows.Forms.View.Details;
            this.lvPoints.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnColumnClick);
            // 
            // chTeam
            // 
            this.chTeam.Text = "Team Name";
            this.chTeam.Width = 125;
            // 
            // chRuns
            // 
            this.chRuns.Text = "Runs";
            // 
            // chHomeRuns
            // 
            this.chHomeRuns.Text = "Home Runs";
            this.chHomeRuns.Width = 80;
            // 
            // chRBI
            // 
            this.chRBI.Text = "RBIs";
            // 
            // chSteals
            // 
            this.chSteals.Text = "Steals";
            // 
            // chOBP
            // 
            this.chOBP.Text = "OBP";
            this.chOBP.Width = 75;
            // 
            // chWins
            // 
            this.chWins.Text = "Wins";
            // 
            // chSaves
            // 
            this.chSaves.Text = "Saves";
            // 
            // chStrikeouts
            // 
            this.chStrikeouts.Text = "Strikeouts";
            // 
            // chERA
            // 
            this.chERA.Tag = "asc";
            this.chERA.Text = "ERA";
            // 
            // chWHIP
            // 
            this.chWHIP.Tag = "asc";
            this.chWHIP.Text = "WHIP";
            // 
            // chTotal
            // 
            this.chTotal.Text = "Total";
            // 
            // lvStats
            // 
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lvStats.GridLines = true;
            this.lvStats.Location = new System.Drawing.Point(12, 32);
            this.lvStats.MultiSelect = false;
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(826, 281);
            this.lvStats.TabIndex = 2;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            this.lvStats.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Team Name";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Runs";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Home Runs";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "RBIs";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Steals";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "OBP";
            this.columnHeader6.Width = 76;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Wins";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Saves";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Tag = "";
            this.columnHeader9.Text = "Strikeouts";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Tag = "asc";
            this.columnHeader10.Text = "ERA";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Tag = "asc";
            this.columnHeader11.Text = "WHIP";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Stats";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Points";
            // 
            // StatCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 645);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvStats);
            this.Controls.Add(this.lvPoints);
            this.Name = "StatCenter";
            this.Text = "Stat Center";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvPoints;
        private System.Windows.Forms.ColumnHeader chTeam;
        private System.Windows.Forms.ColumnHeader chRuns;
        private System.Windows.Forms.ColumnHeader chHomeRuns;
        private System.Windows.Forms.ColumnHeader chRBI;
        private System.Windows.Forms.ColumnHeader chSteals;
        private System.Windows.Forms.ColumnHeader chOBP;
        private System.Windows.Forms.ColumnHeader chWins;
        private System.Windows.Forms.ColumnHeader chSaves;
        private System.Windows.Forms.ColumnHeader chStrikeouts;
        private System.Windows.Forms.ColumnHeader chERA;
        private System.Windows.Forms.ColumnHeader chWHIP;
        private System.Windows.Forms.ListView lvStats;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader chTotal;
    }
}