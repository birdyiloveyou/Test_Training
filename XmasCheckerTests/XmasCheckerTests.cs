using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmasChecker.Tests
{
    [TestClass()]
    public class XmasCheckerTests
    {
        [TestMethod()]
        public void new_xmas()
        {
            var xmasChecker = new FakeChecker();
            xmasChecker.SetDate(2019, 1, 1);

            var actual = xmasChecker.IsTodayXmas();

            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Today_is_not_xmas()
        {
            var xmasChecker = new FakeChecker();
            xmasChecker.SetDate(2019, 1, 1);

            var actual = xmasChecker.IsTodayXmas();

            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void Today_is_xmas()
        {
            var xmasChecker = new FakeChecker();
            xmasChecker.SetDate(2019, 12, 25);

            var actual = xmasChecker.IsTodayXmas();

            Assert.AreEqual(true, actual);
        }

        public class FakeChecker : XmasChecker
        {
            public virtual void SetDate(int i, int i1, int i2)
            {
                today = new DateTime(i, i1, i2);
            }
        }
    }
}