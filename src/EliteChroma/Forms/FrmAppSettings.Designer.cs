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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Keyboard");
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtGameInstall = new System.Windows.Forms.TextBox();
            this.txtGameOptions = new System.Windows.Forms.TextBox();
            this.txtJournal = new System.Windows.Forms.TextBox();
            this.grpEDFolders = new System.Windows.Forms.GroupBox();
            this.btnJournal = new System.Windows.Forms.Button();
            this.lblJournal = new System.Windows.Forms.Label();
            this.btnGameOptions = new System.Windows.Forms.Button();
            this.lblGameOptions = new System.Windows.Forms.Label();
            this.btnGameInstall = new System.Windows.Forms.Button();
            this.lblGameInstall = new System.Windows.Forms.Label();
            this.linkGameFolders = new System.Windows.Forms.LinkLabel();
            this.tvSections = new System.Windows.Forms.TreeView();
            this.pbError = new System.Windows.Forms.PictureBox();
            this.pnlKeyboard = new System.Windows.Forms.Panel();
            this.lblEnUSOverride = new System.Windows.Forms.Label();
            this.chEnUSOverride = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpEDFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
            this.pnlKeyboard.SuspendLayout();
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
            this.btnCancel.Location = new System.Drawing.Point(661, 308);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 32);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(568, 308);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 32);
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
            this.txtGameInstall.Location = new System.Drawing.Point(10, 55);
            this.txtGameInstall.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGameInstall.Name = "txtGameInstall";
            this.txtGameInstall.ReadOnly = true;
            this.txtGameInstall.Size = new System.Drawing.Size(432, 27);
            this.txtGameInstall.TabIndex = 2;
            this.txtGameInstall.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameInstall_Validating);
            this.txtGameInstall.Validated += new System.EventHandler(this.TxtGameInstall_Validated);
            // 
            // txtGameOptions
            // 
            this.txtGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.txtGameOptions, -20);
            this.txtGameOptions.Location = new System.Drawing.Point(10, 120);
            this.txtGameOptions.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGameOptions.Name = "txtGameOptions";
            this.txtGameOptions.ReadOnly = true;
            this.txtGameOptions.Size = new System.Drawing.Size(432, 27);
            this.txtGameOptions.TabIndex = 5;
            this.txtGameOptions.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameOptions_Validating);
            this.txtGameOptions.Validated += new System.EventHandler(this.TxtGameOptions_Validated);
            // 
            // txtJournal
            // 
            this.txtJournal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.txtJournal, -20);
            this.txtJournal.Location = new System.Drawing.Point(10, 185);
            this.txtJournal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtJournal.Name = "txtJournal";
            this.txtJournal.ReadOnly = true;
            this.txtJournal.Size = new System.Drawing.Size(432, 27);
            this.txtJournal.TabIndex = 8;
            this.txtJournal.Validating += new System.ComponentModel.CancelEventHandler(this.TxtJournal_Validating);
            this.txtJournal.Validated += new System.EventHandler(this.TxtJournal_Validated);
            // 
            // grpEDFolders
            // 
            this.grpEDFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpEDFolders.Location = new System.Drawing.Point(202, 13);
            this.grpEDFolders.Margin = new System.Windows.Forms.Padding(3, 4, 3, 8);
            this.grpEDFolders.Name = "grpEDFolders";
            this.grpEDFolders.Padding = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.grpEDFolders.Size = new System.Drawing.Size(546, 277);
            this.grpEDFolders.TabIndex = 1;
            this.grpEDFolders.TabStop = false;
            this.grpEDFolders.Text = "Elite:Dangerous folders";
            // 
            // btnJournal
            // 
            this.btnJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJournal.Location = new System.Drawing.Point(450, 183);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(86, 32);
            this.btnJournal.TabIndex = 9;
            this.btnJournal.Text = "Select...";
            this.btnJournal.UseVisualStyleBackColor = true;
            this.btnJournal.Click += new System.EventHandler(this.BtnJournal_Click);
            // 
            // lblJournal
            // 
            this.lblJournal.AutoSize = true;
            this.lblJournal.Location = new System.Drawing.Point(10, 160);
            this.lblJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.lblJournal.Name = "lblJournal";
            this.lblJournal.Size = new System.Drawing.Size(103, 20);
            this.lblJournal.TabIndex = 7;
            this.lblJournal.Text = "&Journal folder:";
            // 
            // btnGameOptions
            // 
            this.btnGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameOptions.Location = new System.Drawing.Point(450, 117);
            this.btnGameOptions.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnGameOptions.Name = "btnGameOptions";
            this.btnGameOptions.Size = new System.Drawing.Size(86, 32);
            this.btnGameOptions.TabIndex = 6;
            this.btnGameOptions.Text = "Select...";
            this.btnGameOptions.UseVisualStyleBackColor = true;
            this.btnGameOptions.Click += new System.EventHandler(this.BtnGameOptions_Click);
            // 
            // lblGameOptions
            // 
            this.lblGameOptions.AutoSize = true;
            this.lblGameOptions.Location = new System.Drawing.Point(10, 95);
            this.lblGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.lblGameOptions.Name = "lblGameOptions";
            this.lblGameOptions.Size = new System.Drawing.Size(149, 20);
            this.lblGameOptions.TabIndex = 4;
            this.lblGameOptions.Text = "Game &options folder:";
            // 
            // btnGameInstall
            // 
            this.btnGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameInstall.Location = new System.Drawing.Point(450, 52);
            this.btnGameInstall.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnGameInstall.Name = "btnGameInstall";
            this.btnGameInstall.Size = new System.Drawing.Size(86, 32);
            this.btnGameInstall.TabIndex = 3;
            this.btnGameInstall.Text = "Select...";
            this.btnGameInstall.UseVisualStyleBackColor = true;
            this.btnGameInstall.Click += new System.EventHandler(this.BtnGameInstall_Click);
            // 
            // lblGameInstall
            // 
            this.lblGameInstall.AutoSize = true;
            this.lblGameInstall.Location = new System.Drawing.Point(10, 29);
            this.lblGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.lblGameInstall.Name = "lblGameInstall";
            this.lblGameInstall.Size = new System.Drawing.Size(172, 20);
            this.lblGameInstall.TabIndex = 1;
            this.lblGameInstall.Text = "&Game installation folder:";
            // 
            // linkGameFolders
            // 
            this.linkGameFolders.AutoSize = true;
            this.linkGameFolders.LinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.Location = new System.Drawing.Point(10, 231);
            this.linkGameFolders.Name = "linkGameFolders";
            this.linkGameFolders.Size = new System.Drawing.Size(373, 20);
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
            this.tvSections.Location = new System.Drawing.Point(12, 12);
            this.tvSections.Margin = new System.Windows.Forms.Padding(3, 3, 9, 3);
            this.tvSections.Name = "tvSections";
            treeNode1.Name = "GameFolders";
            treeNode1.Text = "Game folders";
            treeNode2.Name = "Keyboard";
            treeNode2.Text = "Keyboard";
            this.tvSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.tvSections.ShowLines = false;
            this.tvSections.ShowRootLines = false;
            this.tvSections.Size = new System.Drawing.Size(178, 278);
            this.tvSections.TabIndex = 0;
            this.tvSections.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TvSections_DrawNode);
            this.tvSections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvSections_AfterSelect);
            // 
            // pbError
            // 
            this.pbError.Image = global::EliteChroma.Properties.Resources.RedDot;
            this.pbError.Location = new System.Drawing.Point(12, 296);
            this.pbError.Name = "pbError";
            this.pbError.Size = new System.Drawing.Size(16, 16);
            this.pbError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbError.TabIndex = 14;
            this.pbError.TabStop = false;
            this.pbError.Visible = false;
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKeyboard.Controls.Add(this.lblEnUSOverride);
            this.pnlKeyboard.Controls.Add(this.chEnUSOverride);
            this.pnlKeyboard.Location = new System.Drawing.Point(202, 12);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Size = new System.Drawing.Size(546, 278);
            this.pnlKeyboard.TabIndex = 2;
            // 
            // lblEnUSOverride
            // 
            this.lblEnUSOverride.AutoSize = true;
            this.lblEnUSOverride.Location = new System.Drawing.Point(20, 31);
            this.lblEnUSOverride.MaximumSize = new System.Drawing.Size(520, 0);
            this.lblEnUSOverride.Name = "lblEnUSOverride";
            this.lblEnUSOverride.Size = new System.Drawing.Size(517, 60);
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
            this.chEnUSOverride.Name = "chEnUSOverride";
            this.chEnUSOverride.Size = new System.Drawing.Size(251, 24);
            this.chEnUSOverride.TabIndex = 0;
            this.chEnUSOverride.Text = "&Force US English keyboard layout";
            this.chEnUSOverride.UseVisualStyleBackColor = true;
            // 
            // FrmAppSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(760, 357);
            this.Controls.Add(this.pnlKeyboard);
            this.Controls.Add(this.pbError);
            this.Controls.Add(this.tvSections);
            this.Controls.Add(this.grpEDFolders);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.AppSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpEDFolders.ResumeLayout(false);
            this.grpEDFolders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
            this.pnlKeyboard.ResumeLayout(false);
            this.pnlKeyboard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button btnGameInstall;
        private System.Windows.Forms.TextBox txtGameInstall;
        private System.Windows.Forms.Label lblGameInstall;
        private System.Windows.Forms.LinkLabel linkGameFolders;
        private System.Windows.Forms.TreeView tvSections;
        private System.Windows.Forms.PictureBox pbError;
        private System.Windows.Forms.Panel pnlKeyboard;
        private System.Windows.Forms.Label lblEnUSOverride;
        private System.Windows.Forms.CheckBox chEnUSOverride;
    }
}