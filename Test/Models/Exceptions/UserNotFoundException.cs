using System.Runtime.Serialization;

namespace Test.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
            throw new UserNotFoundException("Verify your username/password");
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
