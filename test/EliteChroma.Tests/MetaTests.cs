using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Xunit;

namespace EliteChroma.Tests
{
    public class MetaTests
    {
        public static IEnumerable<object[]> GetAllForms()
        {
            var allForms = (from t in typeof(Program).Assembly.GetTypes()
                            where t.IsSubclassOf(typeof(Form))
                            select new object[] { t }).ToList();

            Assert.NotEmpty(allForms);

            return allForms;
        }

        [Theory]
        [MemberData(nameof(GetAllForms))]
        public void PictureBoxesInFormsHaveImages(Type frmType)
        {
            /*
             * Visual Studio's Windows Forms designer (preview) sometimes "forgets"
             * the assignment of image resources to PictureBox controls.
             */

            _ = frmType ?? throw new ArgumentNullException(nameof(frmType));
            using var frm = (Form)frmType.GetConstructor(Type.EmptyTypes)!.Invoke(null);

            var pbs = from fld in frmType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                      where fld.FieldType == typeof(PictureBox)
                      select (PictureBox?)fld.GetValue(frm);

            var noImg = from pb in pbs
                        where pb.Image == null
                        select pb.Name;

            Assert.Empty(noImg);
        }
    }
}
