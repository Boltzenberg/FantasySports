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
            this.lvStats = new System.Windows.Forms.ListView();
            this.lvPoints = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvStats
            // 
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.Location = new System.Drawing.Point(12, 12);
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(1216, 351);
            this.lvStats.TabIndex = 0;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            this.lvStats.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnColumnClick);
            this.lvStats.SelectedIndexChanged += new System.EventHandler(this.OnLaunchTeamStatCenter);
            // 
            // lvPoints
            // 
            this.lvPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPoints.Location = new System.Drawing.Point(12, 366);
            this.lvPoints.Name = "lvPoints";
            this.lvPoints.Size = new System.Drawing.Size(1216, 351);
            this.lvPoints.TabIndex = 1;
            this.lvPoints.UseCompatibleStateImageBehavior = false;
            this.lvPoints.View = System.Windows.Forms.View.Details;
            this.lvPoints.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnColumnClick);
            this.lvPoints.SelectedIndexChanged += new System.EventHandler(this.OnLaunchTeamStatCenter);
            // 
            // StatCenter2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 729);
            this.Controls.Add(this.lvPoints);
            this.Controls.Add(this.lvStats);
            this.Name = "StatCenter2";
            this.Text = "Stat Center";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvStats;
        private System.Windows.Forms.ListView lvPoints;
    }
}