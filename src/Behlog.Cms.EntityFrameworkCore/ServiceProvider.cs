using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Stores;
using Behlog.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    /// <summary>
    /// Adds BehlogDbContext to the Service Collections.
    /// </summary>
    public static IServiceCollection AddBehlogDbContext(this IServiceCollection services)
    {
        services.AddDbContext<IBehlogDbContext, BehlogDbContext>();
        return services;
    }
    

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
        services.AddScoped<IBlockReadStore, BlockReadStore>();
        services.AddScoped<IFileUploadReadStore, FileUploadReadStore>();
        services.AddScoped<ILanguageReadStore, LanguageReadStore>();
        services.AddScoped<ITagReadStore, TagReadStore>();
        services.AddScoped<IWebsiteReadStore, WebsiteReadStore>();
        
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
        services.AddScoped<IBlockWriteStore, BlockWriteStore>();
        services.AddScoped<IFileUploadWriteStore, FileUploadWriteStore>();
        services.AddScoped<ILanguageWriteStore, LanguageWriteStore>();
        services.AddScoped<ITagWriteStore, TagWriteStore>();
        services.AddScoped<IWebsiteWriteStore, WebsiteWriteStore>();
        
        return services;
    }
}