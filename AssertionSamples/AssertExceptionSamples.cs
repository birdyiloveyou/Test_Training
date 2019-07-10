using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AssertionSamples
{
    [TestClass]
    public class AssertExceptionSample
    {
        [TestMethod]
        public void Divide_positive()
        {
            var calculator = new Calculator();

            var actual = calculator.Divide(5, 2);

            Assert.AreEqual(2.5m, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(YouShallNotPassException))]
        public void Divide_Zero()
        {
            var calculator = new Calculator();

            var actual = calculator.Divide(5, 0);

            //how to assert expected exception?
        }
    }

    public class Calculator
    {
        public decimal Divide(decimal first, decimal second)
        {
            if (second == 0)
            {
                throw new YouShallNotPassException();
            }
            return first / second;
        }
    }

    public class YouShallNotPassException : Exception
    {
    }
}