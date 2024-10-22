using System.Text.Json;
using Test.Models;
using Test.Models.Exceptions;
using Test.Utils;

namespace UnitTest
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void SessionManagerTest()
        {
            Assert.ThrowsException<UserNotConnectedException>(() => { SessionService.IsConnected(""); });

            LoginModel user = new() 
            {
                UserId = 1,
                UserName = "b"
            };

            Assert.AreEqual(true,SessionService.IsConnected(JsonSerializer.Serialize(user)));
        }
    }
}
