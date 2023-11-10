using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTest
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var expected = "(10 + 20i)";
            var result = a.ToString();
            Assert.AreEqual(expected, result);
            expected = "(1 + 2i)";
            result = b.ToString();
            Assert.AreEqual(expected, result);

            a = new ComplexNumber()
            {
                Real = 1,
                Imaginary = -1
            };
            b = new ComplexNumber() { Real = 0, Imaginary = 0 };
            shouldBe = new ComplexNumber() { Real = 1, Imaginary = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            expected = "(1 + -1i)";
            result = a.ToString();
            Assert.AreEqual(expected, result);

            expected = "(0 + 0i)";
            result = b.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial polygon = new Polynomial();
            polygon.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polygon.Coefficients.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polygon.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            ComplexNumber result = polygon.Evaluate(new ComplexNumber() { Real = 0, Imaginary = 0 });
            var expected = new ComplexNumber() { Real = 1, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polygon.Evaluate(new ComplexNumber() { Real = 1, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 2, Imaginary = 0 };
            Assert.AreEqual(expected, result);
            result = polygon.Evaluate(new ComplexNumber() { Real = 2, Imaginary = 0 });
            expected = new ComplexNumber() { Real = 5.0000000000, Imaginary = 0 };
            Assert.AreEqual(expected, result);

            var secondResult = polygon.ToString();
            var secondExpected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(secondExpected, secondResult);
        }
    }
}


