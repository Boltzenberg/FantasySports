namespace ScratchUI
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbAuthCode = new System.Windows.Forms.TextBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.wbOut = new System.Windows.Forms.WebBrowser();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set Auth Code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbAuthCode
            // 
            this.tbAuthCode.Location = new System.Drawing.Point(22, 22);
            this.tbAuthCode.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbAuthCode.Name = "tbAuthCode";
            this.tbAuthCode.Size = new System.Drawing.Size(180, 29);
            this.tbAuthCode.TabIndex = 1;
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(22, 70);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(1258, 29);
            this.tbUrl.TabIndex = 2;
            this.tbUrl.Text = "https://fantasysports.yahooapis.com/fantasy/v2/league/404.l.18043/players;start=0" +
    ";count=10;sort=OR";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1294, 65);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 42);
            this.button2.TabIndex = 3;
            this.button2.Text = "Get Resource";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // wbOut
            // 
            this.wbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbOut.Location = new System.Drawing.Point(22, 118);
            this.wbOut.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.wbOut.MinimumSize = new System.Drawing.Size(37, 37);
            this.wbOut.Name = "wbOut";
            this.wbOut.Size = new System.Drawing.Size(1423, 690);
            this.wbOut.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(411, 18);
            this.button3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 42);
            this.button3.TabIndex = 5;
            this.button3.Text = "League";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(559, 18);
            this.button4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(138, 42);
            this.button4.TabIndex = 6;
            this.button4.Text = "Players";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(708, 18);
            this.button5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(211, 42);
            this.button5.TabIndex = 7;
            this.button5.Text = "Merge With ESPN";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 831);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.wbOut);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.tbAuthCode);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbAuthCode;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.WebBrowser wbOut;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

