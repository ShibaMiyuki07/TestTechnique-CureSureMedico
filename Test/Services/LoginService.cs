using Test.Models;
using MySql;
using Test.Models.Exceptions;
using MySql.Data.MySqlClient;

namespace Test.Services
{
    public class LoginService(IConfiguration configuration)
    {
        private readonly string connectionStrings = configuration.GetConnectionString("Mysql")!;

        /*
            Function created to retrieve the user from database
         */
        public async Task<LoginModel> Login(LoginModel user)
        {
            //Get all user with same name (supposed to get only one since the username is unique)
            string query = "SELECT * FROM login where username=@username and password=@password";
            try
            {
                MySqlConnection connection = new(connectionStrings);
                await connection.OpenAsync();


                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("username", user.UserName);
                cmd.Parameters.AddWithValue("password", user.Password);


                await using var reader = await cmd.ExecuteReaderAsync();


                if (!reader.HasRows)
                    throw new UserNotFoundException();

                while (await reader.ReadAsync())
                {
                    LoginModel toCheck = new LoginModel().FromReader(reader);
                    return toCheck;
                }
            }
            catch (MySqlException)
            {
                //Throw an exception if we got an connection error from the connection string in appsettings
                throw new ConnectionException();
            }
            catch
            {
                throw;
            }
            throw new UserNotFoundException();
        }
    }
}
