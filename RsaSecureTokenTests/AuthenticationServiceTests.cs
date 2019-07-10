using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsaSecureToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            var rrr = Substitute.For<RsaTokenDao>();
            var ppp = Substitute.For<ProfileDao>();
            ppp.GetRegisterTimeInMinutes(Arg.Any<string>()).ReturnsForAnyArgs(1);
            rrr.GetRandom(Arg.Any<int>()).ReturnsForAnyArgs(new Random(200));
            var sut = new AuthenticationService(ppp, rrr);

            var actual = sut.IsValid("", "211305");

            Assert.IsTrue(actual);
        }
    }
}