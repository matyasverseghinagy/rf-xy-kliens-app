using System.Windows.Forms;

namespace ClientAppUnitTests
{
    [Category("Készletmódosítás")]
    public class InventoryModificationTests : FormTestsBase
    {
        [TestCase(0, 1)]
        public void TestAddToLocalInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox3, form.localInvAdd_Click);
        }

        [TestCase(1, 0)]
        [TestCase(0, 0)]
        public void TestSubtractFromLocalInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox3, form.localInvSub_Click);
        }

        [TestCase(0, 1)]
        public void TestAddToOnlineInventory(int baseAmount, int expectedAmount)
        {
            TestInventoryModification(baseAmount, expectedAmount, form.textBox4, form.onlineInvAdd_Click);
        }

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
    }
}
