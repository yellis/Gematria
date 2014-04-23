using System;
using NUnit.Framework;

namespace EllisWeb.Gematria.Tests
{
    [TestFixture]
    public class Calculator_Tests
    {

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetGematriaValue_NullString_ThrowsArgumentNullException()
        {
            Calculator.GetGematriaValue(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetGematriaValue_EmptyString_ThrowsArgumentNullException()
        {
            Calculator.GetGematriaValue(string.Empty);
        }

        [Test]
        public void GetGematriaValue_StringWithNoHebrew_ReturnsZero()
        {
            var val = Calculator.GetGematriaValue("absdefg");
            Assert.AreEqual(0, val);
        }

        [Test]
        public void GetGematriaValue_OneWordHebrew_NoNonHebrew_Absolute_ReturnsCorrectValue()
        {
            const string input = "אבגתץ";
            const long expected = 1 + 2 + 3 + 400 + 90;
            var val = Calculator.GetGematriaValue(input);
            Assert.AreEqual(expected, val);
        }

        [Test]
        public void GetGematriaValue_MultipleWords_NoNonHebrew_Absolute_ReturnsCorrectValue()
        {
            const string input = "מנץ אבגת";
            const long expected = 1 + 2 + 3 + 400 + 40 + 50 + 90;
            var val = Calculator.GetGematriaValue(input);
            Assert.AreEqual(expected, val);
        }

        [Test]
        public void GetGematriaValue_OneWordHebrew_WithNonHebrew_Absolute_ReturnsCorrectValue()
        {
            const string input = " jk f.אבגת";
            const long expected = 1 + 2 + 3 + 400;
            var val = Calculator.GetGematriaValue(input);
            Assert.AreEqual(expected, val);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNumericGematriaValue_MultipleWords_ThrowsError()
        {
            string input = "תשעד גב";
            Calculator.GetNumericGematriaValue(input);
        }

        [Test]
        public void GetNumericGematriaValue_OneThousandsGrouping_ReturnsAccurateAnswer()
        {
            string input = "תשעד";
            long expected = 774;
            long output = Calculator.GetNumericGematriaValue(input);
            Assert.AreEqual(expected, output);
        }

        [TestCase("אי", 1010)]
        [TestCase("יצ", 10090)]
        [TestCase("תכמ", 420040)]
        [TestCase("קאי", 101010)]
        public void GetNumericGematriaValue_IllegalOrderOfDigits_ThrowsFormatExceptionInStrictMode(string pattern, long expectedNonStrict)
        {
            bool wasFormatException = false;
            try
            {
                Calculator.GetNumericGematriaValue(pattern, isStrictMode: true);
            }
            catch (FormatException)
            {
                wasFormatException = true;
            }
            Assert.IsTrue(wasFormatException);
            Assert.AreEqual(expectedNonStrict, Calculator.GetNumericGematriaValue(pattern));
        }

        [TestCase("רחצ", 298)]
        public void GetNumericGematriaValue_StrictMode_IllegalOrderOfDigits_NoErrorWhenOnExceptionsList(string pattern, long expected)
        {
            long output = Calculator.GetNumericGematriaValue(pattern, isStrictMode:true);
            Assert.AreEqual(output, expected);
        }

        [Test]
        public void GetNumericGematriaValue_TwoThousandsGrouping_ReturnsAccurateAnswer()
        {
            string input = "התשעד";
            long expected = 5774;
            long output = Calculator.GetNumericGematriaValue(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void GetNumericGematriaValue_TwoThousandsGrouping_WithPunctuation_ReturnsAccurateAnswer()
        {
            string input = "ה'תשע\"ד";
            long expected = 5774;
            long output = Calculator.GetNumericGematriaValue(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void GetNumericGematriaValue_ThreeThousandsGrouping_ReturnsAccurateAnswer()
        {
            string input = "כב'רמג'ללה";
            long expected = 22243065;
            long output = Calculator.GetNumericGematriaValue(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_Zero_ReturnsEmptyString()
        {
            int input = 0;
            string expected = string.Empty;
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertToGematriaNumericString_NegativeNumber_ThrowsException()
        {
            int input = -1;
            Calculator.ConvertToGematriaNumericString(input);
        }

        [Test]
        public void ConvertToGematriaNumericString_SingleDigit_NoSeparators_ReturnsCorrectString()
        {
            int input = 5;
            string expected = "ה";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_TwoDigit_NoSeparators_ReturnsCorrectString()
        {
            int input = 12;
            string expected = "יב";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_TwoDigit_WithSeparators_ReturnsCorrectString()
        {
            int input = 12;
            string expected = "י\"ב";
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_Fifteen_NoSeparators_ReturnsCorrectString()
        {
            int input = 15;
            string expected = "טו";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_Fifteen_WithSeparators_ReturnsCorrectString()
        {
            int input = 15;
            string expected = "ט\"ו";
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_Sixteen_NoSeparators_ReturnsCorrectString()
        {
            int input = 16;
            string expected = "טז";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_ThreeDigit_NoSeparators_ReturnsCorrectString()
        {
            int input = 245;
            string expected = "רמה";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_ThreeDigit_WithSeparators_ReturnsCorrectString()
        {
            int input = 613;
            string expected = "תרי\"ג";
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_FourDigit_NoSeparators_ReturnsCorrectString()
        {
            int input = 5767;
            string expected = "התשסז";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_FourDigit_WithSeparators_ReturnsCorrectString()
        {
            int input = 5767;
            string expected = "ה'תשס\"ז";
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_SevenDigit_NoSeparators_ReturnsCorrectString()
        {
            int input = 1024999;
            string expected = "אכדתתקצט";
            string output = Calculator.ConvertToGematriaNumericString(input, false);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ConvertToGematriaNumericString_SevenDigit_WithSeparators_ReturnsCorrectString()
        {
            int input = 1024999;
            string expected = "א'כד'תתקצ\"ט";
            string output = Calculator.ConvertToGematriaNumericString(input);
            Assert.AreEqual(expected, output);
        }

    }
}
