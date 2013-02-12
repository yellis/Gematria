using NUnit.Framework;

namespace EllisWeb.Gematria.Tests
{
    [TestFixture]
    public class LookupFactory_Tests
    {

        [Test]
        public void TestLookup_Absolute_ContainsExpectedEntries()
        {
            var lookup = LookupFactory.GetDictionary(GematriaType.Absolute);

            Assert.IsNotNull(lookup);
            Assert.AreEqual(1, lookup['א']);
            Assert.AreEqual(20, lookup['כ']);
            Assert.AreEqual(400, lookup['ת']);
            Assert.AreEqual(90, lookup['ץ']);
        }

        [Test]
        public void TestLookup_AbsoluteAlternate_ContainsExpectedEntries()
        {
            var lookup = LookupFactory.GetDictionary(GematriaType.AbsoluteAlternate);

            Assert.IsNotNull(lookup);
            Assert.AreEqual(1, lookup['א']);
            Assert.AreEqual(20, lookup['כ']);
            Assert.AreEqual(400, lookup['ת']);
            Assert.AreEqual(900, lookup['ץ']);
        }

        [Test]
        public void TestLookup_Ordinal_ContainsExpectedEntries()
        {
            var lookup = LookupFactory.GetDictionary(GematriaType.Ordinal);

            Assert.IsNotNull(lookup);
            Assert.AreEqual(1, lookup['א']);
            Assert.AreEqual(11, lookup['כ']);
            Assert.AreEqual(22, lookup['ת']);
            Assert.AreEqual(27, lookup['ץ']);
        }

        [Test]
        public void TestLookup_Reduced_ContainsExpectedEntries_NonSofiyot()
        {
            var lookup = LookupFactory.GetDictionary(GematriaType.Reduced);

            Assert.IsNotNull(lookup);
            Assert.AreEqual(1, lookup['א']);
            Assert.AreEqual(1, lookup['י']);
            Assert.AreEqual(2, lookup['כ']);
            Assert.AreEqual(1, lookup['ק']);
            Assert.AreEqual(4, lookup['ת']);
        }

        [Test]
        public void TestLookup_Reduced_ContainsExpectedEntries_Sofiyot()
        {
            var lookup = LookupFactory.GetDictionary(GematriaType.Reduced);

            Assert.IsNotNull(lookup);
            Assert.AreEqual(5, lookup['ך']);
            Assert.AreEqual(9, lookup['ץ']);
        }

    }
}
