namespace EliteChroma.Controls
{
    partial class RgbPicker
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
            this.tlpRgb = new System.Windows.Forms.TableLayoutPanel();
            this.nudB = new System.Windows.Forms.NumericUpDown();
            this.pbB = new System.Windows.Forms.PictureBox();
            this.lblB = new System.Windows.Forms.Label();
            this.nudG = new System.Windows.Forms.NumericUpDown();
            this.pbG = new System.Windows.Forms.PictureBox();
            this.lblG = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.pbR = new System.Windows.Forms.PictureBox();
            this.nudR = new System.Windows.Forms.NumericUpDown();
            this.pbPicked = new System.Windows.Forms.PictureBox();
            this.tlpRgb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicked)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRgb
            // 
            this.tlpRgb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRgb.ColumnCount = 3;
            this.tlpRgb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpRgb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRgb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRgb.Controls.Add(this.nudB, 2, 2);
            this.tlpRgb.Controls.Add(this.pbB, 1, 2);
            this.tlpRgb.Controls.Add(this.lblB, 0, 2);
            this.tlpRgb.Controls.Add(this.nudG, 2, 1);
            this.tlpRgb.Controls.Add(this.pbG, 1, 1);
            this.tlpRgb.Controls.Add(this.lblG, 0, 1);
            this.tlpRgb.Controls.Add(this.lblR, 0, 0);
            this.tlpRgb.Controls.Add(this.pbR, 1, 0);
            this.tlpRgb.Controls.Add(this.nudR, 2, 0);
            this.tlpRgb.Controls.Add(this.pbPicked, 1, 3);
            this.tlpRgb.Location = new System.Drawing.Point(0, 0);
            this.tlpRgb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpRgb.Name = "tlpRgb";
            this.tlpRgb.RowCount = 4;
            this.tlpRgb.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRgb.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRgb.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRgb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpRgb.Size = new System.Drawing.Size(220, 103);
            this.tlpRgb.TabIndex = 0;
            // 
            // nudB
            // 
            this.nudB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudB.Location = new System.Drawing.Point(126, 56);
            this.nudB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudB.Name = "nudB";
            this.nudB.Size = new System.Drawing.Size(91, 23);
            this.nudB.TabIndex = 8;
            this.nudB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudB.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            // 
            // pbB
            // 
            this.pbB.BackColor = System.Drawing.Color.Blue;
            this.pbB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbB.Location = new System.Drawing.Point(28, 58);
            this.pbB.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pbB.Name = "pbB";
            this.pbB.Size = new System.Drawing.Size(93, 19);
            this.pbB.TabIndex = 1;
            this.pbB.TabStop = false;
            this.pbB.Paint += new System.Windows.Forms.PaintEventHandler(this.PbSlide_Paint);
            this.pbB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            this.pbB.MouseEnter += new System.EventHandler(this.PbSlide_MouseEnter);
            this.pbB.MouseLeave += new System.EventHandler(this.PbSlide_MouseLeave);
            this.pbB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblB.Location = new System.Drawing.Point(3, 58);
            this.lblB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(20, 19);
            this.lblB.TabIndex = 6;
            this.lblB.Text = "B:";
            this.lblB.UseMnemonic = false;
            // 
            // nudG
            // 
            this.nudG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudG.Location = new System.Drawing.Point(126, 29);
            this.nudG.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudG.Name = "nudG";
            this.nudG.Size = new System.Drawing.Size(91, 23);
            this.nudG.TabIndex = 5;
            this.nudG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudG.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            // 
            // pbG
            // 
            this.pbG.BackColor = System.Drawing.Color.Lime;
            this.pbG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbG.Location = new System.Drawing.Point(28, 31);
            this.pbG.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pbG.Name = "pbG";
            this.pbG.Size = new System.Drawing.Size(93, 19);
            this.pbG.TabIndex = 1;
            this.pbG.TabStop = false;
            this.pbG.Paint += new System.Windows.Forms.PaintEventHandler(this.PbSlide_Paint);
            this.pbG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            this.pbG.MouseEnter += new System.EventHandler(this.PbSlide_MouseEnter);
            this.pbG.MouseLeave += new System.EventHandler(this.PbSlide_MouseLeave);
            this.pbG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblG.Location = new System.Drawing.Point(3, 31);
            this.lblG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(20, 19);
            this.lblG.TabIndex = 3;
            this.lblG.Text = "G:";
            this.lblG.UseMnemonic = false;
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblR.Location = new System.Drawing.Point(3, 4);
            this.lblR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(20, 19);
            this.lblR.TabIndex = 0;
            this.lblR.Text = "R:";
            this.lblR.UseMnemonic = false;
            // 
            // pbR
            // 
            this.pbR.BackColor = System.Drawing.Color.Red;
            this.pbR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbR.Location = new System.Drawing.Point(28, 4);
            this.pbR.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pbR.Name = "pbR";
            this.pbR.Size = new System.Drawing.Size(93, 19);
            this.pbR.TabIndex = 1;
            this.pbR.TabStop = false;
            this.pbR.Paint += new System.Windows.Forms.PaintEventHandler(this.PbSlide_Paint);
            this.pbR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            this.pbR.MouseEnter += new System.EventHandler(this.PbSlide_MouseEnter);
            this.pbR.MouseLeave += new System.EventHandler(this.PbSlide_MouseLeave);
            this.pbR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbSlide_MouseDownMove);
            // 
            // nudR
            // 
            this.nudR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudR.Location = new System.Drawing.Point(126, 2);
            this.nudR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudR.Name = "nudR";
            this.nudR.Size = new System.Drawing.Size(91, 23);
            this.nudR.TabIndex = 2;
            this.nudR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudR.ValueChanged += new System.EventHandler(this.Nud_ValueChanged);
            // 
            // pbPicked
            // 
            this.pbPicked.BackColor = System.Drawing.Color.Black;
            this.pbPicked.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRgb.SetColumnSpan(this.pbPicked, 2);
            this.pbPicked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPicked.Location = new System.Drawing.Point(31, 85);
            this.pbPicked.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pbPicked.Name = "pbPicked";
            this.pbPicked.Size = new System.Drawing.Size(184, 14);
            this.pbPicked.TabIndex = 3;
            this.pbPicked.TabStop = false;
            this.pbPicked.Paint += new System.Windows.Forms.PaintEventHandler(this.PbPicked_Paint);
            // 
            // RgbPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpRgb);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RgbPicker";
            this.Size = new System.Drawing.Size(220, 103);
            this.Load += new System.EventHandler(this.RgbPicker_Load);
            this.tlpRgb.ResumeLayout(false);
            this.tlpRgb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicked)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRgb;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.PictureBox pbR;
        private System.Windows.Forms.NumericUpDown nudR;
        private System.Windows.Forms.NumericUpDown nudB;
        private System.Windows.Forms.PictureBox pbB;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.NumericUpDown nudG;
        private System.Windows.Forms.PictureBox pbG;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.PictureBox pbPicked;
    }
}
