using RaktarKeszletDasHaus.Models;

namespace ClientAppUnitTests
{
    [Category("Szűrés")]
    public class FilterTests : FormTestsBase
    {
        [TestCase("Mosógép")]
        public void TestProductNameFilter(string filterExpression)
        {
            form.textBox1.Text = filterExpression;

            var products = form.DGBindigSource.Cast<TermekAdatok>();

            var filteredOutProducts = products.Where(p => !p.ProductNameColumn.Contains(form.textBox1.Text, StringComparison.OrdinalIgnoreCase));

            Assert.That(filteredOutProducts.Count(), Is.EqualTo(0));
        }

        [TestCase("129")]
        public void TestSKUFilter(string filterExpression)
        {
            form.textBox2.Text = filterExpression;

            var products = form.DGBindigSource.Cast<TermekAdatok>();

            var filteredOutProducts = products.Where(p => !p.SKUColumn.Contains(form.textBox2.Text, StringComparison.OrdinalIgnoreCase));

            Assert.That(filteredOutProducts.Count(), Is.EqualTo(0));
        }
    }
}
