using EQUTech.Core.Services.JokerBoard;
using EQUTech.Core.Tasks;
using EQUTech.JokerBoard.Data;
using EQUTech.JokerBoard.Data.PostgreSQL;
using EQUTech.JokerBoard.Services;
using EQUTech.JokerBoard.Web.GrpcServices;
using EQUTech.JokerBoard.Web.Stream;
using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using System.IO.Compression;
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

        services.AddSingleton<StreamManager>();
        services.AddSingleton<StreamHandler>();

        services.AddHostedService
        (
            options => new PingTask
            (
                _configuration.GetValue<TimeSpan>("PingDelay"),
                new()
                {
                    options.GetRequiredService<StreamHandler>()
                },
                options.GetRequiredService<ILogger<PingTask>>()
            )
        );

        services.AddGrpc(options =>
        {
            options.ResponseCompressionLevel = CompressionLevel.Optimal;
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<StreamService>();
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
