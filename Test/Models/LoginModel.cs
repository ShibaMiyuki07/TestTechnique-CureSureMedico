using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Test.Models
{
    public class LoginModel
    {
        public int UserId {  get; set; }
        public string UserName { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        #region FromReader
        public LoginModel FromReader(DbDataReader reader)
        {
            return new LoginModel()
            {
                UserId = reader.GetInt32("userid"),
                UserName = reader.GetString("username")
            };
        }
        #endregion
    }
}
