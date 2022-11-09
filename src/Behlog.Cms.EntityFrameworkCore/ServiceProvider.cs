using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Stores;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds EntityFramework providers for Read Store contracts of Behlog.Cms
    /// </summary>
    public static IServiceCollection AddBehlogCmsEntityFrameworkCoreReadStores(
        this IServiceCollection services)
    {
        services.AddScoped<IContentTypeReadStore, ContentTypeReadStore>();
        services.AddScoped<IContentCategoryReadStore, ContentCategoryReadStore>();
        services.AddScoped<IContentReadStore, ContentReadStore>();
        services.AddScoped<ICommentReadStore, CommentReadStore>();
        
        return services;
    }

    /// <summary>
    /// Adds EntityFramework providers for Write Store contracts of Behlog.Cms
    /// </summary>
    public static IServiceCollection AddBehlogCmsEntityFrameworkCoreWriteStores(
        this IServiceCollection services)
    {
        services.AddScoped<IContentTypeWriteStore, ContentTypeWriteStore>();
        services.AddScoped<IContentCategoryWriteStore, ContentCategoryWriteStore>();
        services.AddScoped<IContentWriteStore, ContentWriteStore>();
        services.AddScoped<ICommentWriteStore, CommentWriteStore>();

        return services;
    }
}