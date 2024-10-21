using Npgsql;
using System.Data;

namespace Test.Models
{
    public class LoginModel
    {
        public int UserId {  get; set; }
        public string UserName { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        #region FromReader
        public LoginModel FromReader(NpgsqlDataReader reader)
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
