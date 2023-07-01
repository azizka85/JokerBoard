using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EQUTech.JokerBoard.Web.Test;

public static class ConfigurationManager
{
    static ConfigurationManager()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

        Configuration = builder.Build();

        var databaseBuilder = CreateBuilder();

        ConnectionString = databaseBuilder.ConnectionString;
    }

    public static IConfiguration Configuration { get; }

    public static string ConnectionString { get; }

    public static NpgsqlConnectionStringBuilder CreateBuilder()
    {
        var databaseConfig = Configuration.GetSection("Database");

        return new()
        {
            Host = databaseConfig?["Host"],
            Port = databaseConfig?["Port"] != null ? int.Parse(databaseConfig["Port"]!) : 5432,
            Database = databaseConfig?["Database"],
            Username = databaseConfig?["Username"],
            Password = databaseConfig?["Password"]
        };
    }
}
