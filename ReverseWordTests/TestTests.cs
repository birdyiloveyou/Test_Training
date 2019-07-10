using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReverseWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseWord.Tests
{
    [TestClass()]
    public class TestTests
    {
        [TestMethod()]
        public void input_empty_return_oneZero()
        {
            var test = new Test();
            var s = "12345";

            var actual = test.AppenZeroOne(s);

            Assert.AreEqual(s + "01", actual);
        }
    }
}