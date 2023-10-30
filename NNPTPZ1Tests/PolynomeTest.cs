using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1Tests
{
    [TestClass()]
    public class PolynomeTest
    {

        private Polynomial polynome;
        [TestInitialize]
        public void TestInitialize()
        {
            polynome = new Polynomial();
            polynome.ComplexNumbersList.Add(new ComplexNumber() { Real = 1, Imaginari = 0 });
            polynome.ComplexNumbersList.Add(new ComplexNumber() { Real = 0, Imaginari = 0 });
            polynome.ComplexNumbersList.Add(new ComplexNumber() { Real = 1, Imaginari = 0 });
        }

        [TestMethod()]
        public void EvaluateTest()
        {
            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { Real = 0, Imaginari = 0 });
            ComplexNumber expected = new ComplexNumber() { Real = 1, Imaginari = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluateTest2()
        {
            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { Real = 1, Imaginari = 0 });
            ComplexNumber expected = new ComplexNumber() { Real = 2, Imaginari = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluateTest3()
        {
            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { Real = 2, Imaginari = 0 });
            ComplexNumber expected = new ComplexNumber() { Real = 5.0000000000, Imaginari = 0 };
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string realString = polynome.ToString();
            string expectedString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expectedString, realString);
        }
    }
}
