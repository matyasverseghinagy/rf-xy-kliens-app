using RaktarKeszletDasHaus;
using RaktarKeszletDasHaus.Models;
using System.Windows.Forms;

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

        [Category("Szűrők törlése")]
        [TestCase("Mock Product Name")]
        public void TestProductNameFilterClear(string filterExpression)
        {
            form.textBox1.Text = filterExpression;

            form.clear_productname_Click(this, EventArgs.Empty);

            Assert.That(form.textBox1.Text, Is.EqualTo(string.Empty));
        }

        [TestCase("Mock SKU")]
        [Category("Szűrők törlése")]
        public void TestSKUFilterClear(string filterExpression)
        {
            form.textBox2.Text = filterExpression;

            form.button1_Click(this, EventArgs.Empty);

            Assert.That(form.textBox2.Text, Is.EqualTo(string.Empty));
        }

        [Category("Készletmódosítás")]
        [TestCase(0, 1)]
        public void TestAddToLocalInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox3, form.localInvAdd_Click);
        }

        [Category("Készletmódosítás")]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        public void TestSubtractFromLocalInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox3, form.localInvSub_Click);
        }

        [Category("Készletmódosítás")]
        [TestCase(0, 1)]
        public void TestAddToOnlineInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox4, form.onlineInvAdd_Click);
        }

        [Category("Készletmódosítás")]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        public void TestSubtractFromOnlineInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox4, form.onlineInvSub_Click);
        }

        // alapmetódus készletmódosítás-tesztekre
        private void TestInventoryModification(int baseAmount, int expectedAmount, TextBox txt, Action<object, EventArgs> modify)
        {
            // CurrentCell megadása kell, hogy az eseménykiszolgáló ne dobjon hibát
            // csak megjelenített cellákra lehet állítani, emiatt .Cells[2] (0 és 1 láthatatlan oszlop)
            form.dataGridView1.CurrentCell = form.dataGridView1.Rows[0].Cells[2];

            txt.Text = baseAmount.ToString();

            modify.Invoke(this, EventArgs.Empty);

            Assert.That(int.Parse(txt.Text), Is.EqualTo(expectedAmount));
        }

        [Category("Szűrés")]
        [TestCase("Mosógép")]
        public void TestProductNameFilter(string filterExpression)
        {
            form.textBox1.Text = filterExpression;

            var products = form.DGBindigSource.Cast<TermekAdatok>();

            var filteredOutProducts = products.Where(p => !p.ProductNameColumn.Contains(form.textBox1.Text, StringComparison.OrdinalIgnoreCase));

            Assert.That(filteredOutProducts.Count(), Is.EqualTo(0));
        }

        [Category("Szűrés")]
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