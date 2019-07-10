using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServerApiDependency.Enum;
using ServerApiDependency.Utility.CustomException;

namespace ServerApiDependency.Tests
{
    [TestClass()]
    public class ServerApiTests
    {
        /// <summary>
        /// LV 3, verify specific method be called
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_exception_test()
        {
            // Assert SaveFailRequestToDb() be called once
            var fake = Substitute.For<ServerApi>();
            fake.Response(Arg.Any<string>()).ReturnsForAnyArgs(0);

            fake.CancelGame();

            fake.Received(1).Response(Arg.Any<string>());
        }

        /// <summary>
        /// LV 2, verify specific exception be thrown
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(AuthFailException))]
        public void post_cancelGame_third_party_fail_test()
        {
            var fake = Substitute.For<ServerApi>();
            fake.When(x => x.TiDebug(Arg.Any<string>(), Arg.Any<int>())).Do(x => { });
            fake.Response(Arg.Any<string>()).ReturnsForAnyArgs(99);

            fake.CancelGame();
            // Assert PostToThirdParty() return not correct should throw AuthFailException
        }

        /// <summary>
        /// LV 1, verify api correct response
        /// </summary>
        [TestMethod()]
        public void post_cancelGame_third_party_success_test()
        {
            // Assert success
            var fake = Substitute.For<ServerApi>();
            fake.Response(Arg.Any<string>()).ReturnsForAnyArgs(0);
            const ServerResponse expected = ServerResponse.Correct;

            var actual = (ServerResponse)fake.Response(Arg.Any<string>());

            Assert.AreEqual(expected, actual);
        }
    }
}