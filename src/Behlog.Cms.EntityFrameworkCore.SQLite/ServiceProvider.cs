using Behlog.Cms.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.SQLite;
using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds Behlog CMS DbContext for SQLite database using EntityFrameworkCore.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbConfig">Database Config for Behlog CMS.</param>
    /// <returns></returns>
    public static IServiceCollection AddBehlogCmsEntityFrameworkCoreSQLite(
        this IServiceCollection services, BehlogDbConfig dbConfig)
    {
        dbConfig.ThrowExceptionIfArgumentIsNull(nameof(dbConfig));
        services.AddTransient<IBehlogDbContext>(
            provider => provider.GetRequiredService<BehlogDbContext>());
        services.AddTransient<IBehlogEntityFrameworkDbContext>(
            provider => provider.GetRequiredService<BehlogDbContext>());
        services.AddEntityFrameworkSqlite();
        services.AddDbContextPool<BehlogDbContext, BehlogSQLiteDbContext>(
            (provider, optionsBuilder)
                => optionsBuilder.Configure(dbConfig, provider));
        
        return services;
    }


    public static void Configure(
        this DbContextOptionsBuilder optionsBuilder,
        BehlogDbConfig dbConfig,
        IServiceProvider serviceProvider)
    {
        optionsBuilder.ThrowExceptionIfArgumentIsNull(nameof(optionsBuilder));
        dbConfig.ThrowExceptionIfArgumentIsNull(nameof(dbConfig));

        optionsBuilder.UseSqlite(dbConfig.ConnectionString,
            __ =>
            {
                __.CommandTimeout((int)TimeSpan.FromMinutes(dbConfig.Timeout).TotalSeconds);
                __.MigrationsAssembly(typeof(BehlogSQLiteDbContext).Assembly.FullName);
            });
        optionsBuilder.UseInternalServiceProvider(serviceProvider);
    }
}