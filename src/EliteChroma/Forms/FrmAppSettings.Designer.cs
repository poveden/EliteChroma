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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tvSections = new System.Windows.Forms.TreeView();
            this.pgColors = new EliteChroma.Forms.Pages.PageColors();
            this.pgGeneral = new EliteChroma.Forms.Pages.PageGeneral();
            this.pgKeyboard = new EliteChroma.Forms.Pages.PageKeyboard();
            this.pgGameFolders = new EliteChroma.Forms.Pages.PageGameFolders();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(578, 289);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 6;
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
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
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
            // pgColors
            // 
            this.pgColors.Colors = null;
            this.pgColors.Location = new System.Drawing.Point(177, 9);
            this.pgColors.Name = "pgColors";
            this.pgColors.Size = new System.Drawing.Size(478, 266);
            this.pgColors.TabIndex = 4;
            // 
            // pgGeneral
            // 
            this.pgGeneral.DetectGameInForeground = false;
            this.pgGeneral.Location = new System.Drawing.Point(177, 9);
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.Size = new System.Drawing.Size(478, 267);
            this.pgGeneral.TabIndex = 2;
            // 
            // pgKeyboard
            // 
            this.pgKeyboard.ForceEnUSKeyboardLayout = false;
            this.pgKeyboard.Location = new System.Drawing.Point(177, 9);
            this.pgKeyboard.Name = "pgKeyboard";
            this.pgKeyboard.Size = new System.Drawing.Size(478, 267);
            this.pgKeyboard.TabIndex = 3;
            // 
            // pgGameFolders
            // 
            this.pgGameFolders.GameInstallFolder = "";
            this.pgGameFolders.GameOptionsFolder = "";
            this.pgGameFolders.JournalFolder = "";
            this.pgGameFolders.Location = new System.Drawing.Point(177, 9);
            this.pgGameFolders.Name = "pgGameFolders";
            this.pgGameFolders.Size = new System.Drawing.Size(478, 267);
            this.pgGameFolders.TabIndex = 1;
            this.pgGameFolders.Error += new System.EventHandler<string>(this.PgGameFolders_Error);
            // 
            // FrmAppSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(665, 326);
            this.Controls.Add(this.tvSections);
            this.Controls.Add(this.pgGameFolders);
            this.Controls.Add(this.pgGeneral);
            this.Controls.Add(this.pgKeyboard);
            this.Controls.Add(this.pgColors);
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TreeView tvSections;
        private Pages.PageColors pgColors;
        private Pages.PageGeneral pgGeneral;
        private Pages.PageKeyboard pgKeyboard;
        private Pages.PageGameFolders pgGameFolders;
    }
}