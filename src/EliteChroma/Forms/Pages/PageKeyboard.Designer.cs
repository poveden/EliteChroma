namespace EliteChroma.Forms.Pages
{
    partial class PageKeyboard
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
            this.lblEnUSOverride = new System.Windows.Forms.Label();
            this.chEnUSOverride = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblEnUSOverride
            // 
            this.lblEnUSOverride.AutoSize = true;
            this.lblEnUSOverride.Location = new System.Drawing.Point(18, 23);
            this.lblEnUSOverride.MaximumSize = new System.Drawing.Size(455, 0);
            this.lblEnUSOverride.Name = "lblEnUSOverride";
            this.lblEnUSOverride.Size = new System.Drawing.Size(440, 45);
            this.lblEnUSOverride.TabIndex = 1;
            this.lblEnUSOverride.Text = "Elite:Dangerous apparently won\'t recognize some keyboard layouts, and will treat " +
    "them as US English. This setting makes EliteChroma properly recognize US English" +
    " keyboard bindings.";
            this.lblEnUSOverride.UseMnemonic = false;
            // 
            // chEnUSOverride
            // 
            this.chEnUSOverride.AutoSize = true;
            this.chEnUSOverride.Location = new System.Drawing.Point(0, 0);
            this.chEnUSOverride.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chEnUSOverride.Name = "chEnUSOverride";
            this.chEnUSOverride.Size = new System.Drawing.Size(201, 19);
            this.chEnUSOverride.TabIndex = 0;
            this.chEnUSOverride.Text = "&Force US English keyboard layout";
            this.chEnUSOverride.UseVisualStyleBackColor = true;
            // 
            // PageKeyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chEnUSOverride);
            this.Controls.Add(this.lblEnUSOverride);
            this.Name = "PageKeyboard";
            this.Size = new System.Drawing.Size(478, 267);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblEnUSOverride;
        private CheckBox chEnUSOverride;
    }
}
