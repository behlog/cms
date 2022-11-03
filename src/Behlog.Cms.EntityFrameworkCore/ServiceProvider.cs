using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Stores;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds EntityFramework providers for different Store contracts of Behlog.Cms
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection AddBehlogCmsEntityFrameworkCore(
        this IServiceCollection services)
    {
        services.AddScoped<IContentTypeReadStore, ContentTypeReadStore>();
        services.AddScoped<IContentTypeWriteStore, ContentTypeWriteStore>();
        services.AddScoped<IContentCategoryReadStore, ContentCategoryReadStore>();
        services.AddScoped<IContentCategoryWriteStore, ContentCategoryWriteStore>();
        services.AddScoped<IContentReadStore, ContentReadStore>();
        services.AddScoped<IContentWriteStore, ContentWriteStore>();
        services.AddScoped<ICommentReadStore, CommentReadStore>();
        services.AddScoped<ICommentWriteStore, CommentWriteStore>();
        
        return services;
    }
}