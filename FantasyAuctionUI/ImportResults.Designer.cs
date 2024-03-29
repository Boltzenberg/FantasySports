﻿
namespace FantasyAuctionUI
{
    partial class ImportResults
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
            this.lvResults = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTeam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPositions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMLBTeam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chMLBTeam,
            this.chPositions,
            this.chTeam,
            this.chCost,
            this.chError});
            this.lvResults.GridLines = true;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(22, 22);
            this.lvResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(2084, 783);
            this.lvResults.TabIndex = 0;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Player Name";
            this.chName.Width = 200;
            // 
            // chTeam
            // 
            this.chTeam.Text = "Fantasy Team";
            this.chTeam.Width = 200;
            // 
            // chCost
            // 
            this.chCost.Text = "Cost";
            this.chCost.Width = 100;
            // 
            // chError
            // 
            this.chError.Text = "Error";
            this.chError.Width = 400;
            // 
            // chPositions
            // 
            this.chPositions.Text = "Positions";
            this.chPositions.Width = 150;
            // 
            // chMLBTeam
            // 
            this.chMLBTeam.Text = "MLB Team";
            this.chMLBTeam.Width = 125;
            // 
            // ImportResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2131, 831);
            this.Controls.Add(this.lvResults);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ImportResults";
            this.Text = "Import Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chTeam;
        private System.Windows.Forms.ColumnHeader chCost;
        private System.Windows.Forms.ColumnHeader chError;
        private System.Windows.Forms.ColumnHeader chMLBTeam;
        private System.Windows.Forms.ColumnHeader chPositions;
    }
}