namespace EliteChroma.Forms.Pages
{
    partial class PageColors
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
            this.pgColors = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgColors
            // 
            this.pgColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgColors.HelpVisible = false;
            this.pgColors.Location = new System.Drawing.Point(0, 0);
            this.pgColors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pgColors.Name = "pgColors";
            this.pgColors.Size = new System.Drawing.Size(478, 267);
            this.pgColors.TabIndex = 0;
            // 
            // PageColors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgColors);
            this.Name = "PageColors";
            this.Size = new System.Drawing.Size(478, 267);
            this.Load += new System.EventHandler(this.PageColors_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyGrid pgColors;
    }
}
