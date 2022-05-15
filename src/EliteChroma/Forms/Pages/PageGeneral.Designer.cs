namespace EliteChroma.Forms.Pages
{
    partial class PageGeneral
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageGeneral));
            this.lblDetectGameProcess = new System.Windows.Forms.Label();
            this.chDetectGameProcess = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblDetectGameProcess
            // 
            this.lblDetectGameProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetectGameProcess.Location = new System.Drawing.Point(18, 23);
            this.lblDetectGameProcess.Name = "lblDetectGameProcess";
            this.lblDetectGameProcess.Size = new System.Drawing.Size(440, 111);
            this.lblDetectGameProcess.TabIndex = 1;
            this.lblDetectGameProcess.Text = resources.GetString("lblDetectGameProcess.Text");
            this.lblDetectGameProcess.UseMnemonic = false;
            // 
            // chDetectGameProcess
            // 
            this.chDetectGameProcess.AutoSize = true;
            this.chDetectGameProcess.Location = new System.Drawing.Point(0, 0);
            this.chDetectGameProcess.Name = "chDetectGameProcess";
            this.chDetectGameProcess.Size = new System.Drawing.Size(376, 19);
            this.chDetectGameProcess.TabIndex = 0;
            this.chDetectGameProcess.Text = "&Detect when Elite:Dangerous process is running on the foreground";
            this.chDetectGameProcess.UseVisualStyleBackColor = true;
            // 
            // PageGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chDetectGameProcess);
            this.Controls.Add(this.lblDetectGameProcess);
            this.Name = "PageGeneral";
            this.Size = new System.Drawing.Size(478, 267);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblDetectGameProcess;
        private CheckBox chDetectGameProcess;
    }
}
