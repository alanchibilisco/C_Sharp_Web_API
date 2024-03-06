using dotenv.net;

namespace Desarrollo.Data;

internal class Database
{
    public static string GetDataBase()
    {
        DotEnv.Load();
        string?  connection=Environment.GetEnvironmentVariable("DB_CONNECTION") ?? throw new Exception("DB_CONNECTION variable is not defined");
        System.Console.WriteLine($"###CONNECTION--> {connection}");
        return connection;
    }
}
