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
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealElement = 10,
                ImaginaryElement = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealElement = 1,
                ImaginaryElement = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealElement = 11,
                ImaginaryElement = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber()
            {
                RealElement = 1,
                ImaginaryElement = -1
            };
            b = new ComplexNumber() { RealElement = 0, ImaginaryElement = 0 };
            shouldBe = new ComplexNumber() { RealElement = 1, ImaginaryElement = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial poly = new Polynomial();
            poly.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1, ImaginaryElement = 0 });
            poly.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 0, ImaginaryElement = 0 });
            poly.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1, ImaginaryElement = 0 });
            ComplexNumber result = poly.Eval(new ComplexNumber() { RealElement = 0, ImaginaryElement = 0 });
            var expected = new ComplexNumber() { RealElement = 1, ImaginaryElement = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealElement = 1, ImaginaryElement = 0 });
            expected = new ComplexNumber() { RealElement = 2, ImaginaryElement = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealElement = 2, ImaginaryElement = 0 });
            expected = new ComplexNumber() { RealElement = 5.0000000000, ImaginaryElement = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


