using Test.Models.Exceptions;
using Test.Utils;

namespace UnitTest
{
    [TestClass]
    public class PasswordTest
    {
        [TestMethod]
        public void PasswordEncryptionTest()
        {
            //Check if the hash function return the hash from online 
            Assert.AreEqual("86f7e437faa5a7fce15d1ddcb9eaeaea377667b8", PasswordEncryption.ToHash("a"));

            //Supposed to get an exception if the hash is empty
            Assert.ThrowsException<PasswordException>(() => { PasswordEncryption.ToHash(""); });
        }
    }
}