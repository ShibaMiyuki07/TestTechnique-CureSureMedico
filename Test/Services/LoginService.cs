using Test.Models;
using Npgsql;
using Test.Models.Exceptions;

namespace Test.Services
{
    public class LoginService(IConfiguration configuration)
    {
        private readonly string connectionStrings = configuration.GetConnectionString("Postgres")!;

        /*
            Function created to retrieve the user from database
         */
        public async Task<LoginModel> Login(LoginModel user)
        {
            //Get all user with same name (supposed to get only one since the username is unique)
            string query = "SELECT * FROM login where username=@username and password=@password";
            try
            {
                NpgsqlConnection connection = new (connectionStrings);
                await connection.OpenAsync();


                using var cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("username", user.UserName);
                cmd.Parameters.AddWithValue("password", user.Password);


                await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                
                if (!reader.HasRows)
                    throw new UserNotFoundException();

                while (await reader.ReadAsync())
                {
                    LoginModel toCheck = new LoginModel().FromReader(reader);
                    return toCheck;
                }
            }
            catch
            {
                //Throw an exception if we got an connection error from the connection string in appsettings
                throw new ConnectionException();
            }
            throw new UserNotFoundException();
        }
    }
}
