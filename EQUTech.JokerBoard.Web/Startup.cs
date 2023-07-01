using EQUTech.Core.Services.JokerBoard;
using EQUTech.JokerBoard.Data;
using EQUTech.JokerBoard.Data.PostgreSQL;
using EQUTech.JokerBoard.Services;
using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using System.Text.Json.Serialization;
using CategoryItemServiceCache = EQUTech.Core.Services.Cache.JokerBoard.CategoryItemService;

namespace EQUTech.JokerBoard.Web;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var databaseBuilder = CreateDatabaseBuilder();

        services.AddMemoryCache();

        services.AddSingleton(_ => new CategoryItemRepository(databaseBuilder.ConnectionString));
        services.AddSingleton<ICategoryItemRepository>(options => options.GetRequiredService<CategoryItemRepository>());

        services.AddSingleton<CategoryItemService>();
        services.AddSingleton
        (
            options => new CategoryItemServiceCache
            (
                options.GetRequiredService<IMemoryCache>(),
                options.GetRequiredService<CategoryItemService>(),
                _configuration.GetValue<string>("CategoryItemService:CacheKey")!,
                _configuration.GetValue<TimeSpan>("CategoryItemService:CacheLifeTime")
            )
        );
        services.AddSingleton<ICategoryItemService>(options => options.GetRequiredService<CategoryItemServiceCache>());

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public NpgsqlConnectionStringBuilder CreateDatabaseBuilder()
    {
        var databaseConfig = _configuration.GetSection("Database");

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
