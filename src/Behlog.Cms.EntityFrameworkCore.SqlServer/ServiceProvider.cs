using Behlog.Cms.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.SqlServer;
using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds Behlog CMS DbContext for SQL Server database using EntityFrameworkCore. 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbConfig">Database Config for Behlog CMS</param>
    /// <returns></returns>
    public static IServiceCollection AddBehlogCmsEntityFrameworkCoreSqlServer(
        this IServiceCollection services, BehlogDbConfig dbConfig)
    {
        dbConfig.ThrowExceptionIfArgumentIsNull(nameof(dbConfig));
        services.AddTransient<IBehlogDbContext>(
            provider => provider.GetRequiredService<BehlogDbContext>());
        services.AddEntityFrameworkSqlServer();
        services.AddDbContextPool<BehlogDbContext, BehlogSqlServerDbContext>(
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

        optionsBuilder.UseSqlServer(dbConfig.ConnectionString,
            __ =>
            {
                __.EnableRetryOnFailure();
                __.CommandTimeout(dbConfig.Timeout);
                __.MigrationsAssembly(typeof(BehlogSqlServerDbContext).Assembly.FullName);
            });
        optionsBuilder.UseInternalServiceProvider(serviceProvider);
    }
}