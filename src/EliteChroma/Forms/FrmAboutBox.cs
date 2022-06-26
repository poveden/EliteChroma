using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using EliteChroma.Internal;

namespace EliteChroma.Forms
{
    [ExcludeFromCodeCoverage]
    internal partial class FrmAboutBox : Form
    {
        public FrmAboutBox()
        {
            InitializeComponent();

            var ai = new AssemblyInfo();
            Text = $"About {ai.Title}";
            labelProductName.Text = ai.Product;
            labelVersion.Text = $"Version {ai.Version.ToString(3)}";
            labelCopyright.Text = ai.Copyright;
            linkCompanyName.Text = ai.Company;
            linkCompanyName.Links[0].LinkData = ai.Company;
            textBoxDescription.Text = ai.Description;
        }

        private void LinkCompanyName_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            // Reference: https://stackoverflow.com/a/53245993/400347
            var ps = new ProcessStartInfo((string)e.Link.LinkData)
            {
                UseShellExecute = true,
                Verb = "open",
            };

            Process.Start(ps)?.Dispose();
        }
    }
}
