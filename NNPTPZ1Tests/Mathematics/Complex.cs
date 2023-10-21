using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;

namespace NNPTPZ1Tests.Mathematics
{
    [TestClass()]
    public class ComplexTests
    {
        public void Add()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);

            var result = a + b;
            var expected = new Complex(11, 22);

            Assert.AreEqual(expected, result);
            result = b + a;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Subtract()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);

            var result = a - b;
            var expected = new Complex(9, 18);

            Assert.AreEqual(expected, result);
            result = b - a;
            expected = new Complex(-9, -18);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Multiply()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);

            var result = a * b;
            var expected = new Complex(-30, 40);

            Assert.AreEqual(expected, result);
            result = b * a;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Divide()
        {
            Complex a = new Complex(10, 20);
            Complex b = new Complex(1, 2);
            var result = a / b;
            var expected = new Complex(10, 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ShouldThrowDivideByZero()
        {
            Complex a = new Complex(10, 20);
            var result = a / Complex.Zero;
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Complex a = new Complex(10, 20);
            string expected = "(10 + 20i)";
            Assert.AreEqual(expected, a.ToString ());

            a = Complex.Zero - a;
            expected = "(-10 - 20i)";
            Assert.AreEqual(expected, a.ToString ());
        }
    }
}
