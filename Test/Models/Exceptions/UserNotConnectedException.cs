using System.Runtime.Serialization;

namespace Test.Models.Exceptions
{
    public class UserNotConnectedException : Exception
    {
        public UserNotConnectedException()
        {
            throw new UserNotConnectedException("You need to connect to see this page");
        }

        public UserNotConnectedException(string? message) : base(message)
        {
        }

        public UserNotConnectedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
