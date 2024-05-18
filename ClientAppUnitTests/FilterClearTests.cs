using RaktarKeszletDasHaus;
using RaktarKeszletDasHaus.Models;
using System.Windows.Forms;

namespace ClientAppUnitTests
{
    [TestFixture]
    [Category("Szűrők törlése")]
    public class FilterClearTests : FormTestsBase
    {
        [TestCase("Vasaló")]
        public void TestProductNameFilterClear(string filterExpression)
        {
            form.textBox1.Text = filterExpression;

            form.clear_productname_Click(this, EventArgs.Empty);

            Assert.That(form.textBox1.Text, Is.EqualTo(string.Empty));
        }

        [TestCase("123456789")]
        public void TestSKUFilterClear(string filterExpression)
        {
            form.textBox2.Text = filterExpression;

            form.button1_Click(this, EventArgs.Empty);

            Assert.That(form.textBox2.Text, Is.EqualTo(string.Empty));
        }
    }
}