using Wspolbiegi0;

namespace Wspolbiegi0Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdditionTest()
        {
            Addition addition = new Addition();
            int[] tablica = { 1, 2, 3 };
            int wynik = addition.Add(tablica);
            Assert.That(wynik, Is.EqualTo(6));
        }
    }
}