using System.Text.Json;
using Test.Models;
using Test.Models.Exceptions;

namespace Test.Utils
{
    public class SessionService
    {
        public static bool IsConnected(string session)
        {
            if (string.IsNullOrEmpty(session))
                throw new UserNotConnectedException();

            LoginModel user = JsonSerializer.Deserialize<LoginModel>(session) ?? throw new UserNotConnectedException();

            if(user.UserId == 0)
                throw new UserNotConnectedException();



            return true;
        }
    }
}
