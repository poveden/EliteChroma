using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using EliteChroma.Internal;

namespace EliteChroma.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class FrmAboutBox : Form
    {
        public FrmAboutBox()
        {
            InitializeComponent();

            var ai = new AssemblyInfo();
            this.Text = $"About {ai.Title}";
            this.labelProductName.Text = ai.Product;
            this.labelVersion.Text = $"Version {ai.Version.ToString(3)}";
            this.labelCopyright.Text = ai.Copyright;
            this.linkCompanyName.Text = ai.Company;
            this.linkCompanyName.Links[0].LinkData = ai.Company;
            this.textBoxDescription.Text = ai.Description;
        }

        private void linkCompanyName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Reference: https://stackoverflow.com/a/53245993/400347
            var ps = new ProcessStartInfo((string)e.Link.LinkData)
            {
                UseShellExecute = true,
                Verb = "open",
            };

            Process.Start(ps);
        }
    }
}
