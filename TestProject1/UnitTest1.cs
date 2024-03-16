using System.ComponentModel;
using Wspolbiegi;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DodawanieTest()
        {
            Dodawanie dodawanie = new Dodawanie();
            int suma = dodawanie.Add(1, 2);
            Assert.AreEqual(3, suma);

        }
    }
}