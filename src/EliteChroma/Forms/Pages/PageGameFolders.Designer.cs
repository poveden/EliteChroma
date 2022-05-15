namespace EliteChroma.Forms.Pages
{
    partial class PageGameFolders
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
            this.components = new System.ComponentModel.Container();
            this.grpEDFolders = new System.Windows.Forms.GroupBox();
            this.lblGameInstall = new System.Windows.Forms.Label();
            this.txtGameInstall = new System.Windows.Forms.TextBox();
            this.btnGameInstall = new EliteChroma.Controls.MenuButton();
            this.ctxGameInstall = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tssGameInstall = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGameInstallBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGameOptions = new System.Windows.Forms.Label();
            this.txtGameOptions = new System.Windows.Forms.TextBox();
            this.btnGameOptions = new System.Windows.Forms.Button();
            this.lblJournal = new System.Windows.Forms.Label();
            this.txtJournal = new System.Windows.Forms.TextBox();
            this.btnJournal = new System.Windows.Forms.Button();
            this.pbInformation = new System.Windows.Forms.PictureBox();
            this.linkGameFolders = new System.Windows.Forms.LinkLabel();
            this.pbWarning = new System.Windows.Forms.PictureBox();
            this.lblPreRunWarning = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.grpEDFolders.SuspendLayout();
            this.ctxGameInstall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEDFolders
            // 
            this.grpEDFolders.Controls.Add(this.lblGameInstall);
            this.grpEDFolders.Controls.Add(this.txtGameInstall);
            this.grpEDFolders.Controls.Add(this.btnGameInstall);
            this.grpEDFolders.Controls.Add(this.lblGameOptions);
            this.grpEDFolders.Controls.Add(this.txtGameOptions);
            this.grpEDFolders.Controls.Add(this.btnGameOptions);
            this.grpEDFolders.Controls.Add(this.lblJournal);
            this.grpEDFolders.Controls.Add(this.txtJournal);
            this.grpEDFolders.Controls.Add(this.btnJournal);
            this.grpEDFolders.Controls.Add(this.pbInformation);
            this.grpEDFolders.Controls.Add(this.linkGameFolders);
            this.grpEDFolders.Controls.Add(this.pbWarning);
            this.grpEDFolders.Controls.Add(this.lblPreRunWarning);
            this.grpEDFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEDFolders.Location = new System.Drawing.Point(0, 0);
            this.grpEDFolders.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.grpEDFolders.Name = "grpEDFolders";
            this.grpEDFolders.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.grpEDFolders.Size = new System.Drawing.Size(478, 267);
            this.grpEDFolders.TabIndex = 11;
            this.grpEDFolders.TabStop = false;
            this.grpEDFolders.Text = "Elite:Dangerous folders";
            // 
            // lblGameInstall
            // 
            this.lblGameInstall.AutoSize = true;
            this.lblGameInstall.Location = new System.Drawing.Point(9, 22);
            this.lblGameInstall.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblGameInstall.Name = "lblGameInstall";
            this.lblGameInstall.Size = new System.Drawing.Size(136, 15);
            this.lblGameInstall.TabIndex = 1;
            this.lblGameInstall.Text = "&Game installation folder:";
            // 
            // txtGameInstall
            // 
            this.txtGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGameInstall.Location = new System.Drawing.Point(9, 41);
            this.txtGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGameInstall.Name = "txtGameInstall";
            this.txtGameInstall.ReadOnly = true;
            this.txtGameInstall.Size = new System.Drawing.Size(378, 23);
            this.txtGameInstall.TabIndex = 2;
            this.txtGameInstall.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameInstall_Validating);
            this.txtGameInstall.Validated += new System.EventHandler(this.TxtGameInstall_Validated);
            // 
            // btnGameInstall
            // 
            this.btnGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameInstall.Location = new System.Drawing.Point(394, 41);
            this.btnGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameInstall.Menu = this.ctxGameInstall;
            this.btnGameInstall.MenuHorizontalDirection = EliteChroma.Controls.MenuButton.HorizontalDirection.Left;
            this.btnGameInstall.Name = "btnGameInstall";
            this.btnGameInstall.Size = new System.Drawing.Size(75, 24);
            this.btnGameInstall.TabIndex = 3;
            this.btnGameInstall.Text = "Select... ";
            this.btnGameInstall.UseVisualStyleBackColor = true;
            // 
            // ctxGameInstall
            // 
            this.ctxGameInstall.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssGameInstall,
            this.tsmiGameInstallBrowse});
            this.ctxGameInstall.Name = "ctxGameInstall";
            this.ctxGameInstall.Size = new System.Drawing.Size(122, 32);
            this.ctxGameInstall.Opening += new System.ComponentModel.CancelEventHandler(this.CtxGameInstall_Opening);
            // 
            // tssGameInstall
            // 
            this.tssGameInstall.Name = "tssGameInstall";
            this.tssGameInstall.Size = new System.Drawing.Size(118, 6);
            // 
            // tsmiGameInstallBrowse
            // 
            this.tsmiGameInstallBrowse.Name = "tsmiGameInstallBrowse";
            this.tsmiGameInstallBrowse.Size = new System.Drawing.Size(121, 22);
            this.tsmiGameInstallBrowse.Text = "Browse...";
            this.tsmiGameInstallBrowse.Click += new System.EventHandler(this.TsmiGameInstallBrowse_Click);
            // 
            // lblGameOptions
            // 
            this.lblGameOptions.AutoSize = true;
            this.lblGameOptions.Location = new System.Drawing.Point(9, 71);
            this.lblGameOptions.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblGameOptions.Name = "lblGameOptions";
            this.lblGameOptions.Size = new System.Drawing.Size(118, 15);
            this.lblGameOptions.TabIndex = 4;
            this.lblGameOptions.Text = "Game &options folder:";
            // 
            // txtGameOptions
            // 
            this.txtGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGameOptions.Location = new System.Drawing.Point(9, 90);
            this.txtGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGameOptions.Name = "txtGameOptions";
            this.txtGameOptions.ReadOnly = true;
            this.txtGameOptions.Size = new System.Drawing.Size(378, 23);
            this.txtGameOptions.TabIndex = 5;
            this.txtGameOptions.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameOptions_Validating);
            this.txtGameOptions.Validated += new System.EventHandler(this.TxtGameOptions_Validated);
            // 
            // btnGameOptions
            // 
            this.btnGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameOptions.Location = new System.Drawing.Point(394, 90);
            this.btnGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameOptions.Name = "btnGameOptions";
            this.btnGameOptions.Size = new System.Drawing.Size(75, 24);
            this.btnGameOptions.TabIndex = 6;
            this.btnGameOptions.Text = "Select...";
            this.btnGameOptions.UseVisualStyleBackColor = true;
            this.btnGameOptions.Click += new System.EventHandler(this.BtnGameOptions_Click);
            // 
            // lblJournal
            // 
            this.lblJournal.AutoSize = true;
            this.lblJournal.Location = new System.Drawing.Point(9, 120);
            this.lblJournal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblJournal.Name = "lblJournal";
            this.lblJournal.Size = new System.Drawing.Size(82, 15);
            this.lblJournal.TabIndex = 7;
            this.lblJournal.Text = "&Journal folder:";
            // 
            // txtJournal
            // 
            this.txtJournal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJournal.Location = new System.Drawing.Point(9, 139);
            this.txtJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtJournal.Name = "txtJournal";
            this.txtJournal.ReadOnly = true;
            this.txtJournal.Size = new System.Drawing.Size(378, 23);
            this.txtJournal.TabIndex = 8;
            this.txtJournal.Validating += new System.ComponentModel.CancelEventHandler(this.TxtJournal_Validating);
            this.txtJournal.Validated += new System.EventHandler(this.TxtJournal_Validated);
            // 
            // btnJournal
            // 
            this.btnJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJournal.Location = new System.Drawing.Point(394, 139);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(75, 24);
            this.btnJournal.TabIndex = 9;
            this.btnJournal.Text = "Select...";
            this.btnJournal.UseVisualStyleBackColor = true;
            this.btnJournal.Click += new System.EventHandler(this.BtnJournal_Click);
            // 
            // pbInformation
            // 
            this.pbInformation.Location = new System.Drawing.Point(9, 173);
            this.pbInformation.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pbInformation.Name = "pbInformation";
            this.pbInformation.Size = new System.Drawing.Size(16, 16);
            this.pbInformation.TabIndex = 14;
            this.pbInformation.TabStop = false;
            // 
            // linkGameFolders
            // 
            this.linkGameFolders.AutoSize = true;
            this.linkGameFolders.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.linkGameFolders.LinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.Location = new System.Drawing.Point(28, 173);
            this.linkGameFolders.Margin = new System.Windows.Forms.Padding(3);
            this.linkGameFolders.MinimumSize = new System.Drawing.Size(0, 16);
            this.linkGameFolders.Name = "linkGameFolders";
            this.linkGameFolders.Size = new System.Drawing.Size(296, 16);
            this.linkGameFolders.TabIndex = 10;
            this.linkGameFolders.TabStop = true;
            this.linkGameFolders.Text = "Default {game install}, {options} and {journal} locations";
            this.linkGameFolders.UseMnemonic = false;
            this.linkGameFolders.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkGameFolders_LinkClicked);
            // 
            // pbWarning
            // 
            this.pbWarning.Location = new System.Drawing.Point(9, 195);
            this.pbWarning.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pbWarning.Name = "pbWarning";
            this.pbWarning.Size = new System.Drawing.Size(16, 16);
            this.pbWarning.TabIndex = 15;
            this.pbWarning.TabStop = false;
            // 
            // lblPreRunWarning
            // 
            this.lblPreRunWarning.AutoSize = true;
            this.lblPreRunWarning.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblPreRunWarning.Location = new System.Drawing.Point(28, 195);
            this.lblPreRunWarning.Margin = new System.Windows.Forms.Padding(3);
            this.lblPreRunWarning.MinimumSize = new System.Drawing.Size(0, 16);
            this.lblPreRunWarning.Name = "lblPreRunWarning";
            this.lblPreRunWarning.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.lblPreRunWarning.Size = new System.Drawing.Size(349, 16);
            this.lblPreRunWarning.TabIndex = 13;
            this.lblPreRunWarning.Text = "The game must have been run at least once for all folders to exist";
            this.lblPreRunWarning.UseMnemonic = false;
            // 
            // folderBrowser
            // 
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // PageGameFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEDFolders);
            this.Name = "PageGameFolders";
            this.Size = new System.Drawing.Size(478, 267);
            this.Load += new System.EventHandler(this.PageGameFolders_Load);
            this.grpEDFolders.ResumeLayout(false);
            this.grpEDFolders.PerformLayout();
            this.ctxGameInstall.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpEDFolders;
        private PictureBox pbWarning;
        private PictureBox pbInformation;
        private Label lblPreRunWarning;
        private Button btnJournal;
        private TextBox txtJournal;
        private Label lblJournal;
        private Button btnGameOptions;
        private TextBox txtGameOptions;
        private Label lblGameOptions;
        private Controls.MenuButton btnGameInstall;
        private TextBox txtGameInstall;
        private Label lblGameInstall;
        private LinkLabel linkGameFolders;
        private FolderBrowserDialog folderBrowser;
        private ContextMenuStrip ctxGameInstall;
        private ToolStripSeparator tssGameInstall;
        private ToolStripMenuItem tsmiGameInstallBrowse;
    }
}
