using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1Tests.Mathematics
{
    [TestClass()]
    public class PolynomialTests
    {
        [TestMethod()]
        public void Evaluate()
        {
            var polynomial = new Polynomial(
                new Complex(1, 0),
                new Complex(0, 0),
                new Complex(1, 0));
            
            
            var result = polynomial.Evaluate(Complex.Zero);
            var expected = new Complex(1, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new Complex(1, 0));
            expected = new Complex(2, 0);
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new Complex(2, 0));
            expected = new Complex(5.0000000000, 0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var polynomial = new Polynomial(
                new Complex(1, 0),
                new Complex(0, 0),
                new Complex(1, 0));

            var result = polynomial.ToString();
            var expected = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(expected, result);
        }
    }
}
