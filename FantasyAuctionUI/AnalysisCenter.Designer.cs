namespace FantasyAuctionUI
{
    partial class AnalysisCenter
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
            this.lbPlayers = new System.Windows.Forms.ListBox();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.lblTeam = new System.Windows.Forms.Label();
            this.cb1B = new System.Windows.Forms.CheckBox();
            this.cb2B = new System.Windows.Forms.CheckBox();
            this.cb3B = new System.Windows.Forms.CheckBox();
            this.cbSS = new System.Windows.Forms.CheckBox();
            this.cbOF = new System.Windows.Forms.CheckBox();
            this.cbC = new System.Windows.Forms.CheckBox();
            this.cbUtil = new System.Windows.Forms.CheckBox();
            this.cbSP = new System.Windows.Forms.CheckBox();
            this.cbRP = new System.Windows.Forms.CheckBox();
            this.cbUndrafted = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbWordWheel
            // 
            this.tbWordWheel.Location = new System.Drawing.Point(13, 13);
            this.tbWordWheel.Name = "tbWordWheel";
            this.tbWordWheel.Size = new System.Drawing.Size(225, 20);
            this.tbWordWheel.TabIndex = 0;
            this.tbWordWheel.TextChanged += new System.EventHandler(this.OnWordWheel);
            // 
            // lbPlayers
            // 
            this.lbPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbPlayers.FormattingEnabled = true;
            this.lbPlayers.Location = new System.Drawing.Point(13, 39);
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(228, 693);
            this.lbPlayers.TabIndex = 1;
            this.lbPlayers.SelectedIndexChanged += new System.EventHandler(this.OnSelectPlayer);
            // 
            // wb
            // 
            this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb.Location = new System.Drawing.Point(247, 39);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(817, 693);
            this.wb.TabIndex = 2;
            // 
            // lblTeam
            // 
            this.lblTeam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTeam.AutoSize = true;
            this.lblTeam.Location = new System.Drawing.Point(244, 16);
            this.lblTeam.Name = "lblTeam";
            this.lblTeam.Size = new System.Drawing.Size(0, 13);
            this.lblTeam.TabIndex = 3;
            // 
            // cb1B
            // 
            this.cb1B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb1B.AutoSize = true;
            this.cb1B.Checked = true;
            this.cb1B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1B.Location = new System.Drawing.Point(1070, 39);
            this.cb1B.Name = "cb1B";
            this.cb1B.Size = new System.Drawing.Size(77, 17);
            this.cb1B.TabIndex = 4;
            this.cb1B.Text = "Include 1B";
            this.cb1B.UseVisualStyleBackColor = true;
            this.cb1B.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cb2B
            // 
            this.cb2B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb2B.AutoSize = true;
            this.cb2B.Checked = true;
            this.cb2B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb2B.Location = new System.Drawing.Point(1070, 62);
            this.cb2B.Name = "cb2B";
            this.cb2B.Size = new System.Drawing.Size(77, 17);
            this.cb2B.TabIndex = 5;
            this.cb2B.Text = "Include 2B";
            this.cb2B.UseVisualStyleBackColor = true;
            this.cb2B.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cb3B
            // 
            this.cb3B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb3B.AutoSize = true;
            this.cb3B.Checked = true;
            this.cb3B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb3B.Location = new System.Drawing.Point(1070, 85);
            this.cb3B.Name = "cb3B";
            this.cb3B.Size = new System.Drawing.Size(77, 17);
            this.cb3B.TabIndex = 6;
            this.cb3B.Text = "Include 3B";
            this.cb3B.UseVisualStyleBackColor = true;
            this.cb3B.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbSS
            // 
            this.cbSS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSS.AutoSize = true;
            this.cbSS.Checked = true;
            this.cbSS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSS.Location = new System.Drawing.Point(1070, 108);
            this.cbSS.Name = "cbSS";
            this.cbSS.Size = new System.Drawing.Size(78, 17);
            this.cbSS.TabIndex = 7;
            this.cbSS.Text = "Include SS";
            this.cbSS.UseVisualStyleBackColor = true;
            this.cbSS.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbOF
            // 
            this.cbOF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOF.AutoSize = true;
            this.cbOF.Checked = true;
            this.cbOF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOF.Location = new System.Drawing.Point(1070, 131);
            this.cbOF.Name = "cbOF";
            this.cbOF.Size = new System.Drawing.Size(78, 17);
            this.cbOF.TabIndex = 8;
            this.cbOF.Text = "Include OF";
            this.cbOF.UseVisualStyleBackColor = true;
            this.cbOF.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbC
            // 
            this.cbC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbC.AutoSize = true;
            this.cbC.Checked = true;
            this.cbC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbC.Location = new System.Drawing.Point(1070, 154);
            this.cbC.Name = "cbC";
            this.cbC.Size = new System.Drawing.Size(71, 17);
            this.cbC.TabIndex = 9;
            this.cbC.Text = "Include C";
            this.cbC.UseVisualStyleBackColor = true;
            this.cbC.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbUtil
            // 
            this.cbUtil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUtil.AutoSize = true;
            this.cbUtil.Checked = true;
            this.cbUtil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUtil.Location = new System.Drawing.Point(1070, 177);
            this.cbUtil.Name = "cbUtil";
            this.cbUtil.Size = new System.Drawing.Size(79, 17);
            this.cbUtil.TabIndex = 10;
            this.cbUtil.Text = "Include Util";
            this.cbUtil.UseVisualStyleBackColor = true;
            this.cbUtil.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbSP
            // 
            this.cbSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSP.AutoSize = true;
            this.cbSP.Checked = true;
            this.cbSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSP.Location = new System.Drawing.Point(1070, 200);
            this.cbSP.Name = "cbSP";
            this.cbSP.Size = new System.Drawing.Size(78, 17);
            this.cbSP.TabIndex = 11;
            this.cbSP.Text = "Include SP";
            this.cbSP.UseVisualStyleBackColor = true;
            this.cbSP.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbRP
            // 
            this.cbRP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRP.AutoSize = true;
            this.cbRP.Checked = true;
            this.cbRP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRP.Location = new System.Drawing.Point(1070, 223);
            this.cbRP.Name = "cbRP";
            this.cbRP.Size = new System.Drawing.Size(79, 17);
            this.cbRP.TabIndex = 12;
            this.cbRP.Text = "Include RP";
            this.cbRP.UseVisualStyleBackColor = true;
            this.cbRP.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // cbUndrafted
            // 
            this.cbUndrafted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUndrafted.AutoSize = true;
            this.cbUndrafted.Checked = true;
            this.cbUndrafted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUndrafted.Location = new System.Drawing.Point(1070, 246);
            this.cbUndrafted.Name = "cbUndrafted";
            this.cbUndrafted.Size = new System.Drawing.Size(97, 17);
            this.cbUndrafted.TabIndex = 13;
            this.cbUndrafted.Text = "Undrafted Only";
            this.cbUndrafted.UseVisualStyleBackColor = true;
            this.cbUndrafted.CheckedChanged += new System.EventHandler(this.OnUpdatePlayerFilter);
            // 
            // AnalysisCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 737);
            this.Controls.Add(this.cbUndrafted);
            this.Controls.Add(this.cbRP);
            this.Controls.Add(this.cbSP);
            this.Controls.Add(this.cbUtil);
            this.Controls.Add(this.cbC);
            this.Controls.Add(this.cbOF);
            this.Controls.Add(this.cbSS);
            this.Controls.Add(this.cb3B);
            this.Controls.Add(this.cb2B);
            this.Controls.Add(this.cb1B);
            this.Controls.Add(this.lblTeam);
            this.Controls.Add(this.wb);
            this.Controls.Add(this.lbPlayers);
            this.Controls.Add(this.tbWordWheel);
            this.Name = "AnalysisCenter";
            this.Text = "Analysis Center";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWordWheel;
        private System.Windows.Forms.ListBox lbPlayers;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.Label lblTeam;
        private System.Windows.Forms.CheckBox cb1B;
        private System.Windows.Forms.CheckBox cb2B;
        private System.Windows.Forms.CheckBox cb3B;
        private System.Windows.Forms.CheckBox cbSS;
        private System.Windows.Forms.CheckBox cbOF;
        private System.Windows.Forms.CheckBox cbC;
        private System.Windows.Forms.CheckBox cbUtil;
        private System.Windows.Forms.CheckBox cbSP;
        private System.Windows.Forms.CheckBox cbRP;
        private System.Windows.Forms.CheckBox cbUndrafted;
    }
}