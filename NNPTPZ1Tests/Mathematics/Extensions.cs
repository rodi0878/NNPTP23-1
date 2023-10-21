using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;

namespace NNPTPZ1Tests.Mathematics
{
    [TestClass()]
    public class ComplexExtensionsTests
    {
        [TestMethod()]
        public void Exponentiate_PositiveExponent()
        {
            Complex @base = new Complex(1, 1);
            int exponent = 2;
            var result = @base.Exponentiate(exponent);
            var expected = new Complex(0, 2);
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void Exponentiate_NegativeExponent()
        {
            Complex @base = new Complex(1, 1);
            int exponent = -2;
            var result = @base.Exponentiate(exponent);
            var expected = new Complex(0, -0.5);
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        public void Exponentiate_ZeroExponentShouldReturnOne()
        {
            Complex @base = Complex.Zero;
            int exponent = 0;
            Assert.AreEqual(Complex.One, @base.Exponentiate(exponent));
        }
        [TestMethod()]
        public void GetReciprocal()
        {
            Complex @base = new Complex(1, 1);
            var result = @base.GetReciprocal();
            var expected = new Complex(0.5, -0.5);
            Assert.AreEqual(expected, result);
        }
        [TestMethod()]
        [ExpectedException(typeof(DivideByZeroException))]
        public void GetReciprocal_ShouldThrowDivideByZero()
        {
            Complex @base = Complex.Zero;
            var result = @base.GetReciprocal();
        }
    }
}

