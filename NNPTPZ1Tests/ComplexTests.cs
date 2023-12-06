using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber addend = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber otherAddend = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actual = addend.Add(otherAddend);
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(expected, actual);

            var expectedString = "(10 + 20i)";
            var actualString = addend.ToString();
            Assert.AreEqual(expectedString, actualString);
            expectedString = "(1 + 2i)";
            actualString = otherAddend.ToString();
            Assert.AreEqual(expectedString, actualString);

            addend = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            otherAddend = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = addend.Add(otherAddend);
            Assert.AreEqual(expected, actual);

            expectedString = "(1 + -1i)";
            actualString = addend.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(0 + 0i)";
            actualString = otherAddend.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynome polynome = new Mathematics.Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynome.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynome.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var actualString = polynome.ToString();
            var expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedString, actualString);
        }
    }
}


