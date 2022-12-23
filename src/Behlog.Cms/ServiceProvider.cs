using Behlog.Cms.Setup;
using Behlog.Cms.Domain;
using Behlog.Cms.Services;
using Behlog.Cms.Contracts;

namespace Microsoft.Extensions.DependencyInjection;


public static class ServiceProvider
{

    /// <summary>
    /// Adds Behlog CMS Commands, Queries and Services to the DI container.
    /// This must be called before using any of Behlog.Cms services.
    /// </summary>
    public static void AddBehlogCms(this IServiceCollection services)
    {
        //var myassembly = typeof(Website).Assembly;
        
        //services.AddBehlogManager(new[]
        //{
        //    myassembly
        //});
        
        //services.AddBehlogMiddleware(new[]
        //{
        //    myassembly
        //});
        
        services.AddBehlogCmsServices();
    }


    private static void AddBehlogCmsServices(this IServiceCollection services)
    {
        services.AddScoped<ICmsSetup, CmsSetup>();
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<IContentTypeService, ContentTypeService>();
        services.AddScoped<IWebsiteService, WebsiteService>();
        services.AddScoped<IComponentService, ComponentService>();
    }
    
}