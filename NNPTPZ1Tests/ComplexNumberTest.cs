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

        ComplexNumber numberTenTwenty;
        ComplexNumber numberOneTwo;

        [TestInitialize]
        public void TestInitialize()
        {
            numberTenTwenty = new ComplexNumber()
            {
                Real = 10,
                Imaginari = 20
            };
            numberOneTwo = new ComplexNumber()
            {
                Real = 1,
                Imaginari = 2
            };
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string expectedString = "(10 + 20i)";
            string realString = numberTenTwenty.ToString();
            Assert.AreEqual(expectedString, realString);
        }

        [TestMethod()]
        public void ToStringTestZero()
        {
            string expectedString = "(0 + 0i)";
            ComplexNumber zero = ComplexNumber.Zero;
            string realString = zero.ToString();
            Assert.AreEqual(expectedString, realString);
        }

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber actual = numberTenTwenty.Add(numberOneTwo);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginari = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }
    }
}


