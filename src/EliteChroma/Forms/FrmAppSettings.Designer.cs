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
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpEDFolders = new System.Windows.Forms.GroupBox();
            this.btnGameInstall = new System.Windows.Forms.Button();
            this.txtGameInstall = new System.Windows.Forms.TextBox();
            this.lblGameInstall = new System.Windows.Forms.Label();
            this.btnGameOptions = new System.Windows.Forms.Button();
            this.txtGameOptions = new System.Windows.Forms.TextBox();
            this.lblGameOptions = new System.Windows.Forms.Label();
            this.btnJournal = new System.Windows.Forms.Button();
            this.txtJournal = new System.Windows.Forms.TextBox();
            this.lblJournal = new System.Windows.Forms.Label();
            this.linkGameFolders = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpEDFolders.SuspendLayout();
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
            this.btnCancel.Location = new System.Drawing.Point(454, 231);
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
            this.btnOK.Location = new System.Drawing.Point(373, 231);
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
            // grpEDFolders
            // 
            this.grpEDFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
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
            this.grpEDFolders.Location = new System.Drawing.Point(12, 12);
            this.grpEDFolders.Controls.Add(this.linkGameFolders);
            this.grpEDFolders.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.grpEDFolders.Name = "grpEDFolders";
            this.grpEDFolders.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.grpEDFolders.Size = new System.Drawing.Size(517, 208);
            this.grpEDFolders.TabIndex = 0;
            this.grpEDFolders.TabStop = false;
            this.grpEDFolders.Text = "Elite:Dangerous folders";
            // 
            // btnGameInstall
            // 
            this.btnGameInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameInstall.Location = new System.Drawing.Point(433, 39);
            this.btnGameInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameInstall.Name = "btnGameInstall";
            this.btnGameInstall.Size = new System.Drawing.Size(75, 24);
            this.btnGameInstall.TabIndex = 3;
            this.btnGameInstall.Text = "Select...";
            this.btnGameInstall.UseVisualStyleBackColor = true;
            this.btnGameInstall.Click += new System.EventHandler(this.BtnGameInstall_Click);
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
            this.txtGameInstall.Size = new System.Drawing.Size(418, 23);
            this.txtGameInstall.TabIndex = 2;
            this.txtGameInstall.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameInstall_Validating);
            this.txtGameInstall.Validated += new System.EventHandler(this.TxtGameInstall_Validated);
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
            // btnGameOptions
            // 
            this.btnGameOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameOptions.Location = new System.Drawing.Point(433, 88);
            this.btnGameOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGameOptions.Name = "btnGameOptions";
            this.btnGameOptions.Size = new System.Drawing.Size(75, 24);
            this.btnGameOptions.TabIndex = 6;
            this.btnGameOptions.Text = "Select...";
            this.btnGameOptions.UseVisualStyleBackColor = true;
            this.btnGameOptions.Click += new System.EventHandler(this.BtnGameOptions_Click);
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
            this.txtGameOptions.Size = new System.Drawing.Size(418, 23);
            this.txtGameOptions.TabIndex = 5;
            this.txtGameOptions.Validating += new System.ComponentModel.CancelEventHandler(this.TxtGameOptions_Validating);
            this.txtGameOptions.Validated += new System.EventHandler(this.TxtGameOptions_Validated);
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
            // btnJournal
            // 
            this.btnJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJournal.Location = new System.Drawing.Point(433, 137);
            this.btnJournal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJournal.Name = "btnJournal";
            this.btnJournal.Size = new System.Drawing.Size(75, 24);
            this.btnJournal.TabIndex = 9;
            this.btnJournal.Text = "Select...";
            this.btnJournal.UseVisualStyleBackColor = true;
            this.btnJournal.Click += new System.EventHandler(this.BtnJournal_Click);
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
            this.txtJournal.Size = new System.Drawing.Size(418, 23);
            this.txtJournal.TabIndex = 8;
            this.txtJournal.Validating += new System.ComponentModel.CancelEventHandler(this.TxtJournal_Validating);
            this.txtJournal.Validated += new System.EventHandler(this.TxtJournal_Validated);
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
            // linkGameFolders
            // 
            this.linkGameFolders.AutoSize = true;
            this.linkGameFolders.LinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.Location = new System.Drawing.Point(9, 173);
            this.linkGameFolders.Name = "linkGameFolders";
            this.linkGameFolders.Size = new System.Drawing.Size(388, 26);
            this.linkGameFolders.TabIndex = 12;
            this.linkGameFolders.TabStop = true;
            this.linkGameFolders.Text = "Default {game install}, {options} and {journal} locations";
            this.linkGameFolders.UseMnemonic = false;
            this.linkGameFolders.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkGameFolders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGameFolders_LinkClicked);
            // 
            // FrmAppSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(541, 268);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox grpEDFolders;
        private System.Windows.Forms.Button btnJournal;
        internal System.Windows.Forms.TextBox txtJournal;
        private System.Windows.Forms.Label lblJournal;
        private System.Windows.Forms.Button btnGameOptions;
        internal System.Windows.Forms.TextBox txtGameOptions;
        private System.Windows.Forms.Label lblGameOptions;
        private System.Windows.Forms.Button btnGameInstall;
        internal System.Windows.Forms.TextBox txtGameInstall;
        private System.Windows.Forms.Label lblGameInstall;
        private System.Windows.Forms.LinkLabel linkGameFolders;
    }
}