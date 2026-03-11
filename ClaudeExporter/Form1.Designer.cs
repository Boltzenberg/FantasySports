namespace ClaudeExporter
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
            this.webview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.btnRoster = new System.Windows.Forms.Button();
            this.btnAuction = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.webview)).BeginInit();
            this.SuspendLayout();
            // 
            // webview
            // 
            this.webview.AllowExternalDrop = true;
            this.webview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webview.CreationProperties = null;
            this.webview.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webview.Location = new System.Drawing.Point(12, 77);
            this.webview.Name = "webview";
            this.webview.Size = new System.Drawing.Size(1236, 1142);
            this.webview.TabIndex = 0;
            this.webview.ZoomFactor = 1D;
            // 
            // btnRoster
            // 
            this.btnRoster.Location = new System.Drawing.Point(12, 12);
            this.btnRoster.Name = "btnRoster";
            this.btnRoster.Size = new System.Drawing.Size(134, 47);
            this.btnRoster.TabIndex = 1;
            this.btnRoster.Text = "Roster";
            this.btnRoster.UseVisualStyleBackColor = true;
            this.btnRoster.Click += new System.EventHandler(this.OnExportRoster);
            // 
            // btnAuction
            // 
            this.btnAuction.Location = new System.Drawing.Point(152, 12);
            this.btnAuction.Name = "btnAuction";
            this.btnAuction.Size = new System.Drawing.Size(134, 47);
            this.btnAuction.TabIndex = 2;
            this.btnAuction.Text = "Auction";
            this.btnAuction.UseVisualStyleBackColor = true;
            this.btnAuction.Click += new System.EventHandler(this.OnExportAuction);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(1281, 12);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(1283, 1207);
            this.tbOutput.TabIndex = 3;
            this.tbOutput.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2565, 1231);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.btnAuction);
            this.Controls.Add(this.btnRoster);
            this.Controls.Add(this.webview);
            this.Name = "Form1";
            this.Text = "Claude Exporter";
            ((System.ComponentModel.ISupportInitialize)(this.webview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webview;
        private System.Windows.Forms.Button btnRoster;
        private System.Windows.Forms.Button btnAuction;
        private System.Windows.Forms.TextBox tbOutput;
    }
}

