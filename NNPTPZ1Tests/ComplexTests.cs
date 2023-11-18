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
    public class ComplexTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex firstAddend = new Complex()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };
            Complex secondAddend = new Complex()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            Complex actual = firstAddend.Add(secondAddend);
            Complex shouldBe = new Complex()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var expectedRepresentation = "(10 + 20i)";
            var actualRepresentation = firstAddend.ToString();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);
            expectedRepresentation = "(1 + 2i)";
            actualRepresentation = secondAddend.ToString();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            firstAddend = new Complex()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };
            secondAddend = new Complex() { RealPart = 0, ImaginaryPart = 0 };
            shouldBe = new Complex() { RealPart = 1, ImaginaryPart = -1 };
            actual = firstAddend.Add(secondAddend);
            Assert.AreEqual(shouldBe, actual);

            expectedRepresentation = "(1 + -1i)";
            actualRepresentation = firstAddend.ToString();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);

            expectedRepresentation = "(0 + 0i)";
            actualRepresentation = secondAddend.ToString();
            Assert.AreEqual(expectedRepresentation, actualRepresentation);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynomial polynomial = new Mathematics.Polynomial (
                new Complex() { 
                    RealPart = 1, ImaginaryPart = 0 }, new Complex() { RealPart = 0, ImaginaryPart = 0 }, new Complex() { RealPart = 1, ImaginaryPart = 0 
                }
            );
            Complex result = polynomial.Evaluate(new Complex() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new Complex() { RealPart = 1, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { RealPart = 1, ImaginaryPart = 0 });
            expected = new Complex() { RealPart = 2, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);
            result = polynomial.Evaluate(new Complex() { RealPart = 2, ImaginaryPart = 0 });
            expected = new Complex() { RealPart = 5.0000000000, ImaginaryPart = 0 };
            Assert.AreEqual(expected, result);

            var actualRepresentation = polynomial.ToString();
            var expectedRepresentation = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedRepresentation, actualRepresentation);
        }
    }
}


