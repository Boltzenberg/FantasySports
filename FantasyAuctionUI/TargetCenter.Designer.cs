namespace FantasyAuctionUI
{
    partial class TargetCenter
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
            this.cbTeams = new System.Windows.Forms.ComboBox();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.cbRP = new System.Windows.Forms.CheckBox();
            this.cbSP = new System.Windows.Forms.CheckBox();
            this.cbUtil = new System.Windows.Forms.CheckBox();
            this.cbC = new System.Windows.Forms.CheckBox();
            this.cbOF = new System.Windows.Forms.CheckBox();
            this.cbSS = new System.Windows.Forms.CheckBox();
            this.cb3B = new System.Windows.Forms.CheckBox();
            this.cb2B = new System.Windows.Forms.CheckBox();
            this.cb1B = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbTeams
            // 
            this.cbTeams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTeams.FormattingEnabled = true;
            this.cbTeams.Location = new System.Drawing.Point(12, 12);
            this.cbTeams.Name = "cbTeams";
            this.cbTeams.Size = new System.Drawing.Size(1233, 21);
            this.cbTeams.TabIndex = 0;
            this.cbTeams.SelectedIndexChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // lvPlayers
            // 
            this.lvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers.Location = new System.Drawing.Point(12, 39);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(1133, 698);
            this.lvPlayers.TabIndex = 1;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            // 
            // cbRP
            // 
            this.cbRP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRP.AutoSize = true;
            this.cbRP.Checked = true;
            this.cbRP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRP.Location = new System.Drawing.Point(1151, 223);
            this.cbRP.Name = "cbRP";
            this.cbRP.Size = new System.Drawing.Size(79, 17);
            this.cbRP.TabIndex = 22;
            this.cbRP.Text = "Include RP";
            this.cbRP.UseVisualStyleBackColor = true;
            this.cbRP.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cbSP
            // 
            this.cbSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSP.AutoSize = true;
            this.cbSP.Checked = true;
            this.cbSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSP.Location = new System.Drawing.Point(1151, 200);
            this.cbSP.Name = "cbSP";
            this.cbSP.Size = new System.Drawing.Size(78, 17);
            this.cbSP.TabIndex = 21;
            this.cbSP.Text = "Include SP";
            this.cbSP.UseVisualStyleBackColor = true;
            this.cbSP.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cbUtil
            // 
            this.cbUtil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUtil.AutoSize = true;
            this.cbUtil.Checked = true;
            this.cbUtil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUtil.Location = new System.Drawing.Point(1151, 177);
            this.cbUtil.Name = "cbUtil";
            this.cbUtil.Size = new System.Drawing.Size(79, 17);
            this.cbUtil.TabIndex = 20;
            this.cbUtil.Text = "Include Util";
            this.cbUtil.UseVisualStyleBackColor = true;
            this.cbUtil.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cbC
            // 
            this.cbC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbC.AutoSize = true;
            this.cbC.Checked = true;
            this.cbC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbC.Location = new System.Drawing.Point(1151, 154);
            this.cbC.Name = "cbC";
            this.cbC.Size = new System.Drawing.Size(71, 17);
            this.cbC.TabIndex = 19;
            this.cbC.Text = "Include C";
            this.cbC.UseVisualStyleBackColor = true;
            this.cbC.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cbOF
            // 
            this.cbOF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOF.AutoSize = true;
            this.cbOF.Checked = true;
            this.cbOF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOF.Location = new System.Drawing.Point(1151, 131);
            this.cbOF.Name = "cbOF";
            this.cbOF.Size = new System.Drawing.Size(78, 17);
            this.cbOF.TabIndex = 18;
            this.cbOF.Text = "Include OF";
            this.cbOF.UseVisualStyleBackColor = true;
            this.cbOF.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cbSS
            // 
            this.cbSS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSS.AutoSize = true;
            this.cbSS.Checked = true;
            this.cbSS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSS.Location = new System.Drawing.Point(1151, 108);
            this.cbSS.Name = "cbSS";
            this.cbSS.Size = new System.Drawing.Size(78, 17);
            this.cbSS.TabIndex = 17;
            this.cbSS.Text = "Include SS";
            this.cbSS.UseVisualStyleBackColor = true;
            this.cbSS.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cb3B
            // 
            this.cb3B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb3B.AutoSize = true;
            this.cb3B.Checked = true;
            this.cb3B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3B.Location = new System.Drawing.Point(1151, 85);
            this.cb3B.Name = "cb3B";
            this.cb3B.Size = new System.Drawing.Size(77, 17);
            this.cb3B.TabIndex = 16;
            this.cb3B.Text = "Include 3B";
            this.cb3B.UseVisualStyleBackColor = true;
            this.cb3B.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cb2B
            // 
            this.cb2B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb2B.AutoSize = true;
            this.cb2B.Checked = true;
            this.cb2B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb2B.Location = new System.Drawing.Point(1151, 62);
            this.cb2B.Name = "cb2B";
            this.cb2B.Size = new System.Drawing.Size(77, 17);
            this.cb2B.TabIndex = 15;
            this.cb2B.Text = "Include 2B";
            this.cb2B.UseVisualStyleBackColor = true;
            this.cb2B.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // cb1B
            // 
            this.cb1B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb1B.AutoSize = true;
            this.cb1B.Checked = true;
            this.cb1B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1B.Location = new System.Drawing.Point(1151, 39);
            this.cb1B.Name = "cb1B";
            this.cb1B.Size = new System.Drawing.Size(77, 17);
            this.cb1B.TabIndex = 14;
            this.cb1B.Text = "Include 1B";
            this.cb1B.UseVisualStyleBackColor = true;
            this.cb1B.CheckedChanged += new System.EventHandler(this.OnTeamSelected);
            // 
            // TargetCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 749);
            this.Controls.Add(this.cbRP);
            this.Controls.Add(this.cbSP);
            this.Controls.Add(this.cbUtil);
            this.Controls.Add(this.cbC);
            this.Controls.Add(this.cbOF);
            this.Controls.Add(this.cbSS);
            this.Controls.Add(this.cb3B);
            this.Controls.Add(this.cb2B);
            this.Controls.Add(this.cb1B);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.cbTeams);
            this.Name = "TargetCenter";
            this.Text = "a";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTeams;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.CheckBox cbRP;
        private System.Windows.Forms.CheckBox cbSP;
        private System.Windows.Forms.CheckBox cbUtil;
        private System.Windows.Forms.CheckBox cbC;
        private System.Windows.Forms.CheckBox cbOF;
        private System.Windows.Forms.CheckBox cbSS;
        private System.Windows.Forms.CheckBox cb3B;
        private System.Windows.Forms.CheckBox cb2B;
        private System.Windows.Forms.CheckBox cb1B;
    }
}