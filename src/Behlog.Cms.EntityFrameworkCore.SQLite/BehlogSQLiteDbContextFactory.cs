using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Behlog.Core.Models;
using Behlog.Extensions;

namespace Behlog.Cms.EntityFrameworkCore.SQLite;

public class BehlogSQLiteDbContextFactory : IDesignTimeDbContextFactory<BehlogDbContext>
{
    public BehlogDbContext CreateDbContext(string[] args)
    {
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ILoggerFactory, LoggerFactory>();
        var basePath = Directory.GetCurrentDirectory();
        Console.WriteLine($"Using '{basePath}' as the ContentRootPath");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, reloadOnChange: true)
            .Build();
        services.AddSingleton<IConfigurationRoot>(provider => configuration);
        services.Configure<BehlogOptions>(options => configuration.Bind(options));
        Console.WriteLine("Configurations binded to options...");
        var options = services.BuildServiceProvider()
            .GetRequiredService<IOptionsSnapshot<BehlogOptions>>()?
            .Value;
        if (options is null)
        {
            Console.WriteLine("Error: BehlogOptions not found!");
            options.ThrowExceptionIfReferenceIsNull(nameof(options));
        }
        Console.WriteLine("BehlogOptions loaded...");
        var dbConfig = options.DbConfig;
        if (dbConfig is null)
        {
            Console.WriteLine("Error: DbConfig is null!");
            dbConfig.ThrowExceptionIfReferenceIsNull(nameof(dbConfig));
        }
        Console.WriteLine($"Connecting to {dbConfig.DbName}...");
        Console.WriteLine($"Using connection string: {dbConfig.ConnectionString}...");
        services.AddEntityFrameworkSqlite();
        var optionsBuilder = new DbContextOptionsBuilder<BehlogDbContext>();
        optionsBuilder.Configure(dbConfig, services.BuildServiceProvider());
        return new BehlogSQLiteDbContext(optionsBuilder.Options);
    }
}