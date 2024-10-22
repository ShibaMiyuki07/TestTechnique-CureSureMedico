using System.Text.Json;
using Test.Models;
using Test.Models.Exceptions;

namespace Test.Utils
{
    public class SessionService
    {
        //Function to check if the session is valid 
        public static bool IsConnected(string session)
        {
            if (string.IsNullOrEmpty(session))
                throw new UserNotConnectedException();

            LoginModel user = GetUserData(session);

            if(user.UserId == 0)
                throw new UserNotConnectedException();
            
            return true;
        }

        //Get data from the session
        public static LoginModel GetUserData(string session) 
        {
            LoginModel user = JsonSerializer.Deserialize<LoginModel>(session) ?? throw new UserNotConnectedException();
            return user;
        }
    }
}
