namespace EliteChroma.Forms
{
    partial class FrmAppSettings
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Game folders");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Keyboard");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Colors");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAppSettings));
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtGameInstall = new System.Windows.Forms.TextBox();
            this.txtGameOptions = new System.Windows.Forms.TextBox();
            this.txtJournal = new System.Windows.Forms.TextBox();
            this.grpEDFolders = new System.Windows.Forms.GroupBox();
            this.pbWarning = new System.Windows.Forms.PictureBox();
            this.pbInformation = new System.Windows.Forms.PictureBox();
            this.lblPreRunWarning = new System.Windows.Forms.Label();
            this.btnJournal = new System.Windows.Forms.Button();
            this.lblJournal = new System.Windows.Forms.Label();
            this.btnGameOptions = new System.Windows.Forms.Button();
            this.lblGameOptions = new System.Windows.Forms.Label();
            this.btnGameInstall = new EliteChroma.Controls.MenuButton();
            this.ctxGameInstall = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tssGameInstall = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGameInstallBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGameInstall = new System.Windows.Forms.Label();
            this.linkGameFolders = new System.Windows.Forms.LinkLabel();
            this.tvSections = new System.Windows.Forms.TreeView();
            this.pnlKeyboard = new System.Windows.Forms.Panel();
            this.lblEnUSOverride = new System.Windows.Forms.Label();
            this.chEnUSOverride = new System.Windows.Forms.CheckBox();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.pgColors = new System.Windows.Forms.PropertyGrid();
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.lblDetectGameProcess = new System.Windows.Forms.Label();
            this.chDetectGameProcess = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpEDFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInformation)).BeginInit();
            this.ctxGameInstall.SuspendLayout();
            this.pnlKeyboard.SuspendLayout();
            this.pnlColors.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowser
            // 
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(578, 289);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(497, 289);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // txtGameInstall
            // 
            this.txtGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.txtGameInstall, -20);
            this.txtGameInstall.Location = new System.Drawing.Point(9, 41);
            this.txtGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGameInstall.Name = "txtGameInstall";
            this.txtGameInstall.ReadOnly = true;
            this.txtGameInstall.Size = new System.Drawing.Size(378, 23);
            this.txtGameInstall.TabIndex = 2;
            this.txtGameInstall.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameInstall_Validating);
            this.txtGameInstall.Validated += new System.EventHandler(this.TxtGameInstall_Validated);
            // 
            // txtGameOptions
            // 
            this.txtGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.txtGameOptions, -20);
            this.txtGameOptions.Location = new System.Drawing.Point(9, 90);
            this.txtGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGameOptions.Name = "txtGameOptions";
            this.txtGameOptions.ReadOnly = true;
            this.txtGameOptions.Size = new System.Drawing.Size(378, 23);
            this.txtGameOptions.TabIndex = 5;
            this.txtGameOptions.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameOptions_Validating);
            this.txtGameOptions.Validated += new System.EventHandler(this.TxtGameOptions_Validated);
            // 
            // txtJournal
            // 
            this.txtJournal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.txtJournal, -20);
            this.txtJournal.Location = new System.Drawing.Point(9, 139);
            this.txtJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtJournal.Name = "txtJournal";
            this.txtJournal.ReadOnly = true;
            this.txtJournal.Size = new System.Drawing.Size(378, 23);
            this.txtJournal.TabIndex = 8;
            this.txtJournal.Validating += new System.ComponentModel.CancelEventHandler(this.TxtJournal_Validating);
            this.txtJournal.Validated += new System.EventHandler(this.TxtJournal_Validated);
            // 
            // grpEDFolders
            // 
            this.grpEDFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEDFolders.Controls.Add(this.pbWarning);
            this.grpEDFolders.Controls.Add(this.pbInformation);
            this.grpEDFolders.Controls.Add(this.lblPreRunWarning);
            this.grpEDFolders.Controls.Add(this.btnJournal);
            this.grpEDFolders.Controls.Add(this.txtJournal);
            this.grpEDFolders.Controls.Add(this.lblJournal);
            this.grpEDFolders.Controls.Add(this.btnGameOptions);
            this.grpEDFolders.Controls.Add(this.txtGameOptions);
            this.grpEDFolders.Controls.Add(this.lblGameOptions);
            this.grpEDFolders.Controls.Add(this.btnGameInstall);
            this.grpEDFolders.Controls.Add(this.txtGameInstall);
            this.grpEDFolders.Controls.Add(this.lblGameInstall);
            this.grpEDFolders.Controls.Add(this.linkGameFolders);
            this.grpEDFolders.Location = new System.Drawing.Point(177, 10);
            this.grpEDFolders.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.grpEDFolders.Name = "grpEDFolders";
            this.grpEDFolders.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.grpEDFolders.Size = new System.Drawing.Size(478, 266);
            this.grpEDFolders.TabIndex = 1;
            this.grpEDFolders.TabStop = false;
            this.grpEDFolders.Text = "Elite:Dangerous folders";
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
            // pbInformation
            // 
            this.pbInformation.Location = new System.Drawing.Point(9, 173);
            this.pbInformation.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pbInformation.Name = "pbInformation";
            this.pbInformation.Size = new System.Drawing.Size(16, 16);
            this.pbInformation.TabIndex = 14;
            this.pbInformation.TabStop = false;
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
            // btnJournal
            // 
            this.btnJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJournal.Location = new System.Drawing.Point(394, 137);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(75, 24);
            this.btnJournal.TabIndex = 9;
            this.btnJournal.Text = "Select...";
            this.btnJournal.UseVisualStyleBackColor = true;
            this.btnJournal.Click += new System.EventHandler(this.BtnJournal_Click);
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
            // btnGameOptions
            // 
            this.btnGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameOptions.Location = new System.Drawing.Point(394, 88);
            this.btnGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameOptions.Name = "btnGameOptions";
            this.btnGameOptions.Size = new System.Drawing.Size(75, 24);
            this.btnGameOptions.TabIndex = 6;
            this.btnGameOptions.Text = "Select...";
            this.btnGameOptions.UseVisualStyleBackColor = true;
            this.btnGameOptions.Click += new System.EventHandler(this.BtnGameOptions_Click);
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
            // btnGameInstall
            // 
            this.btnGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameInstall.Location = new System.Drawing.Point(394, 39);
            this.btnGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameInstall.Menu = this.ctxGameInstall;
            this.btnGameInstall.MenuHorizontalDirection = EliteChroma.Controls.MenuButton.HorizontalDirection.Left;
            this.btnGameInstall.Name = "btnGameInstall";
            this.btnGameInstall.Size = new System.Drawing.Size(75, 24);
            this.btnGameInstall.TabIndex = 3;
            this.btnGameInstall.Text = "Select...";
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
            this.linkGameFolders.TabIndex = 12;
            this.linkGameFolders.TabStop = true;
            this.linkGameFolders.Text = "Default {game install}, {options} and {journal} locations";
            this.linkGameFolders.UseMnemonic = false;
            this.linkGameFolders.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkGameFolders_LinkClicked);
            // 
            // tvSections
            // 
            this.tvSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvSections.FullRowSelect = true;
            this.tvSections.HideSelection = false;
            this.tvSections.Location = new System.Drawing.Point(10, 9);
            this.tvSections.Margin = new System.Windows.Forms.Padding(3, 2, 8, 2);
            this.tvSections.Name = "tvSections";
            treeNode1.Name = "GameFolders";
            treeNode1.Text = "Game folders";
            treeNode2.Name = "General";
            treeNode2.Text = "General";
            treeNode3.Name = "Keyboard";
            treeNode3.Text = "Keyboard";
            treeNode4.Name = "Colors";
            treeNode4.Text = "Colors";
            this.tvSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.tvSections.ShowLines = false;
            this.tvSections.ShowRootLines = false;
            this.tvSections.Size = new System.Drawing.Size(156, 267);
            this.tvSections.TabIndex = 0;
            this.tvSections.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TvSections_DrawNode);
            this.tvSections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvSections_AfterSelect);
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKeyboard.Controls.Add(this.lblEnUSOverride);
            this.pnlKeyboard.Controls.Add(this.chEnUSOverride);
            this.pnlKeyboard.Location = new System.Drawing.Point(177, 9);
            this.pnlKeyboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Size = new System.Drawing.Size(478, 266);
            this.pnlKeyboard.TabIndex = 3;
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
            // pnlColors
            // 
            this.pnlColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlColors.Controls.Add(this.pgColors);
            this.pnlColors.Location = new System.Drawing.Point(177, 9);
            this.pnlColors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(478, 266);
            this.pnlColors.TabIndex = 4;
            // 
            // pgColors
            // 
            this.pgColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgColors.HelpVisible = false;
            this.pgColors.Location = new System.Drawing.Point(0, 0);
            this.pgColors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pgColors.Name = "pgColors";
            this.pgColors.Size = new System.Drawing.Size(478, 266);
            this.pgColors.TabIndex = 2;
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Controls.Add(this.lblDetectGameProcess);
            this.pnlGeneral.Controls.Add(this.chDetectGameProcess);
            this.pnlGeneral.Location = new System.Drawing.Point(177, 9);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(478, 267);
            this.pnlGeneral.TabIndex = 2;
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
            // 
            // chDetectGameProcess
            // 
            this.chDetectGameProcess.AutoSize = true;
            this.chDetectGameProcess.Location = new System.Drawing.Point(0, 0);
            this.chDetectGameProcess.Name = "chDetectGameProcess";
            this.chDetectGameProcess.Size = new System.Drawing.Size(376, 19);
            this.chDetectGameProcess.TabIndex = 0;
            this.chDetectGameProcess.Text = "Detect when Elite:Dangerous process is running on the foreground";
            this.chDetectGameProcess.UseVisualStyleBackColor = true;
            // 
            // FrmAppSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(665, 326);
            this.Controls.Add(this.pnlColors);
            this.Controls.Add(this.pnlGeneral);
            this.Controls.Add(this.pnlKeyboard);
            this.Controls.Add(this.tvSections);
            this.Controls.Add(this.grpEDFolders);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.AppSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpEDFolders.ResumeLayout(false);
            this.grpEDFolders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInformation)).EndInit();
            this.ctxGameInstall.ResumeLayout(false);
            this.pnlKeyboard.ResumeLayout(false);
            this.pnlKeyboard.PerformLayout();
            this.pnlColors.ResumeLayout(false);
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox grpEDFolders;
        private System.Windows.Forms.Button btnJournal;
        private System.Windows.Forms.TextBox txtJournal;
        private System.Windows.Forms.Label lblJournal;
        private System.Windows.Forms.Button btnGameOptions;
        private System.Windows.Forms.TextBox txtGameOptions;
        private System.Windows.Forms.Label lblGameOptions;
        private Controls.MenuButton btnGameInstall;
        private System.Windows.Forms.TextBox txtGameInstall;
        private System.Windows.Forms.Label lblGameInstall;
        private System.Windows.Forms.LinkLabel linkGameFolders;
        private System.Windows.Forms.TreeView tvSections;
        private System.Windows.Forms.Panel pnlKeyboard;
        private System.Windows.Forms.Label lblEnUSOverride;
        private System.Windows.Forms.CheckBox chEnUSOverride;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.PropertyGrid pgColors;
        private System.Windows.Forms.ContextMenuStrip ctxGameInstall;
        private System.Windows.Forms.ToolStripSeparator tssGameInstall;
        private System.Windows.Forms.ToolStripMenuItem tsmiGameInstallBrowse;
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.CheckBox chDetectGameProcess;
        private System.Windows.Forms.Label lblDetectGameProcess;
        private System.Windows.Forms.Label lblPreRunWarning;
        private System.Windows.Forms.PictureBox pbInformation;
        private System.Windows.Forms.PictureBox pbWarning;
    }
}