using RaktarKeszletDasHaus;

namespace ClientAppUnitTests
{
    [TestFixture]
    public abstract class FormTestsBase
    {
        protected Form1 form;

        [OneTimeSetUp]
        public void Setup()
        {
            form = new Form1();
        }
    }
}
