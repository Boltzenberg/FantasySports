namespace FantasyAuctionUI
{
    partial class SnakeCenter
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
            this.lvTarget = new System.Windows.Forms.ListView();
            this.tbWordWheel = new System.Windows.Forms.TextBox();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.lvStats = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvTarget
            // 
            this.lvTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTarget.Location = new System.Drawing.Point(0, 24);
            this.lvTarget.Name = "lvTarget";
            this.lvTarget.Size = new System.Drawing.Size(904, 223);
            this.lvTarget.TabIndex = 0;
            this.lvTarget.UseCompatibleStateImageBehavior = false;
            this.lvTarget.View = System.Windows.Forms.View.Details;
            this.lvTarget.Click += new System.EventHandler(this.OnPlayerSelected);
            // 
            // tbWordWheel
            // 
            this.tbWordWheel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWordWheel.Location = new System.Drawing.Point(0, 2);
            this.tbWordWheel.Name = "tbWordWheel";
            this.tbWordWheel.Size = new System.Drawing.Size(904, 20);
            this.tbWordWheel.TabIndex = 1;
            this.tbWordWheel.TextChanged += new System.EventHandler(this.OnWordWheelChanged);
            // 
            // lvPlayers
            // 
            this.lvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers.Location = new System.Drawing.Point(0, 508);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(904, 250);
            this.lvPlayers.TabIndex = 2;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            // 
            // lvStats
            // 
            this.lvStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStats.Location = new System.Drawing.Point(0, 252);
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(904, 252);
            this.lvStats.TabIndex = 3;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            // 
            // SnakeCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 758);
            this.Controls.Add(this.lvStats);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.tbWordWheel);
            this.Controls.Add(this.lvTarget);
            this.Name = "SnakeCenter";
            this.Text = "SnakeCenter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvTarget;
        private System.Windows.Forms.TextBox tbWordWheel;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.ListView lvStats;
    }
}