using System.Runtime.Serialization;

namespace Test.Models.Exceptions
{
    public class PasswordException : Exception
    {
        public PasswordException()
        {
        }

        public PasswordException(string? message) : base(message)
        {
        }

        public PasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
