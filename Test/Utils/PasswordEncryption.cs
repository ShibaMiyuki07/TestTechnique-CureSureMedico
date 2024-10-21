using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;
using Test.Models.Exceptions;

namespace Test.Utils
{
    public class PasswordEncryption
    {
        public static string ToHash(string password)
        {
            if(!string.IsNullOrEmpty(password))
            {
               return BCrypt.Net.BCrypt.HashPassword(password);
            }
            else
                throw new PasswordException("Password can't be empty");
        }
    }
}
