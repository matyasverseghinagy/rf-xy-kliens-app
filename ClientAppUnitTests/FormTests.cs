using RaktarKeszletDasHaus;

namespace ClientAppUnitTests
{
    [TestFixture]
    public class FormTests
    {
        Form1 form;

        [OneTimeSetUp]
        public void Setup()
        {
            form = new Form1();
        }

        [TestCase("Mock Product Name")]
        [Category("Clear Filters")]
        public void TestProductNameFilterClear(string filterExpression)
        {
            form.textBox1.Text = filterExpression;

            form.clear_productname_Click(this, EventArgs.Empty);

            Assert.That(form.textBox1.Text, Is.EqualTo(string.Empty));
        }

        [TestCase("Mock SKU")]
        [Category("Clear Filters")]
        public void TestSKUFilterClear(string filterExpression)
        {
            form.textBox2.Text = filterExpression;

            form.button1_Click(this, EventArgs.Empty);

            Assert.That(form.textBox2.Text, Is.EqualTo(string.Empty));
        }
    }
}