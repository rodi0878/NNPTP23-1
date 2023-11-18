using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App;
using Mathematics;

namespace Mathematics.Tests
{
    [TestClass()]
    public class AppTests
    {

        [TestMethod()]
        public void AddTestComplexNumber()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealNumber = 10,
                ImaginaryUnit = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryUnit = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealNumber = 11,
                ImaginaryUnit = 22
            };

            Assert.AreEqual(shouldBe, actual);

            string equalsTo = "(10 + 20i)";
            string complexNumberToString = a.ToString();
            Assert.AreEqual(equalsTo, complexNumberToString);

            equalsTo = "(1 + 2i)";
            complexNumberToString = b.ToString();
            Assert.AreEqual(equalsTo, complexNumberToString);

            a = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryUnit = -1
            };
            b = new ComplexNumber()
            {
                RealNumber = 0,
                ImaginaryUnit = 0
            };

            actual = a.Add(b);
            shouldBe = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryUnit = -1
            };
            Assert.AreEqual(shouldBe, actual);

            equalsTo = "(1 + -1i)";
            complexNumberToString = a.ToString();
            Assert.AreEqual(equalsTo, complexNumberToString);

            equalsTo = "(0 + 0i)";
            complexNumberToString = b.ToString();
            Assert.AreEqual(equalsTo, complexNumberToString);
        }

        [TestMethod()]
        public void AddTestPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(
                new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 }
            );
            polynomial.Add(
                new ComplexNumber() { RealNumber = 0, ImaginaryUnit = 0 }
            );
            polynomial.Add(
                new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 }
            );

            ComplexNumber result = polynomial.EvaluateAt(new ComplexNumber() { RealNumber = 0, ImaginaryUnit = 0 });
            ComplexNumber expected = new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.EvaluateAt(new ComplexNumber() { RealNumber = 1, ImaginaryUnit = 0 });
            expected = new ComplexNumber() { RealNumber = 2, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.EvaluateAt(new ComplexNumber() { RealNumber = 2, ImaginaryUnit = 0 });
            expected = new ComplexNumber() { RealNumber = 5.0000000000, ImaginaryUnit = 0 };
            Assert.AreEqual(expected, result);

            var equalsTo = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            var polynomialToString = polynomial.ToString();
            Assert.AreEqual(equalsTo, polynomialToString);
        }
    }
}


