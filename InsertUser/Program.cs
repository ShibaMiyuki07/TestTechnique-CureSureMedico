using MySql.Data.MySqlClient;
using Test.Utils;

MySqlConnection connection = new("Server=sql.freedb.tech;Port=3306;Database=freedb_test_cure;User Id=freedb_manitra;Password=#TRfJ2KZtp7n*UP;");

Console.WriteLine("Enter username :");
string? username = Console.ReadLine();

Console.WriteLine("Enter the password");
string password = "";
Boolean asd = true;
while (asd)
{
    char s = Console.ReadKey(true).KeyChar;
    if (s == '\r')
    {
        asd = false;
    }
    else
    {
        password = password + s.ToString();
    }
}
Console.WriteLine("waiting...");
connection.Open();
string query = "INSERT INTO login(username,password) VALUES(@username,@password)";
using var cmd = new MySqlCommand(query, connection);
cmd.Parameters.AddWithValue("username", username);
cmd.Parameters.AddWithValue(@"password", PasswordEncryption.ToHash(password));


cmd.ExecuteNonQuery();