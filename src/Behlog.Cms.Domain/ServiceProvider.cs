
using System.Runtime.CompilerServices;
using Behlog.Cms.Contracts;
using Behlog.Cms.Domain;
using Behlog.Cms.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds Behlog CMS Commands, Queries and Services to the DI container.
    /// This must be called before using any of Behlog.Cms services.
    /// </summary>
    public static void AddBehlogCms(this IServiceCollection services)
    {
        var myassembly = typeof(Website).Assembly;
        
        services.AddBehlogManager(new[]
        {
            myassembly
        });
        
        services.AddBehlogMiddleware(new[]
        {
            myassembly
        });
        
        services.AddBehlogCmsServices();
    }


    private static void AddBehlogCmsServices(this IServiceCollection services)
    {
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<IContentTypeService, ContentTypeService>();
        services.AddScoped<IWebsiteService, WebsiteService>();
    }
}