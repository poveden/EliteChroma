using System.Windows.Forms;
using EliteChroma.Forms;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class FrmAppSettingsTests
    {
        [Fact]
        public void AllSectionEntriesHaveTheirTagPropertySetToTheCorrespondingControl()
        {
            using var frm = new FrmAppSettings();
            var tvSections = frm.GetPrivateField<TreeView>("tvSections")!;

            foreach (TreeNode node in tvSections.Nodes)
            {
                Assert.NotNull(node.Tag);
                Assert.IsAssignableFrom<Control>(node.Tag);
            }
        }
    }
}
