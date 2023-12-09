using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber firstComplexNumber = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            ComplexNumber secondComplexNumber = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber actual = firstComplexNumber.Add(secondComplexNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);

            string expectedResult = "(10 + 20i)";
            string actualResult = firstComplexNumber.ToString();
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = "(1 + 2i)";
            actualResult = secondComplexNumber.ToString();
            Assert.AreEqual(expectedResult, actualResult);

            firstComplexNumber = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            secondComplexNumber = new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new ComplexNumber() { RealPart = 1, ImaginaryPart = -1 };
            actual = firstComplexNumber.Add(secondComplexNumber);
            Assert.AreEqual(shouldBe, actual);

            expectedResult = "(1 + -1i)";
            actualResult = firstComplexNumber.ToString();
            Assert.AreEqual(expectedResult, actualResult);

            expectedResult = "(0 + 0i)";
            actualResult = secondComplexNumber.ToString();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber result = polynomial.Eval(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Eval(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Eval(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            expected = new ComplexNumber() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            string actualResult = polynomial.ToString();
            string expectedResult = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

