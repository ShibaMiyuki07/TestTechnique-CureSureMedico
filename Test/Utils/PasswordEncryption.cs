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
            if(string.IsNullOrEmpty(password))
                throw new PasswordException("Password can't be empty");
            

            byte[] sourceByte = Encoding.UTF8.GetBytes(password);
            byte[] hashbyte = SHA1.HashData(sourceByte);

            //Convert the byte hashed to string 
            string hash = BitConverter.ToString(hashbyte).Replace("-", String.Empty);
            return hash.ToLower();
            
        }
    }
}
