using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Cms.Repository;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{


    public static IServiceCollection AddBehlogCmsEntityFrameworkCore(
        this IServiceCollection services)
    {
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IContentCategoryRepository, ContentCategoryRepository>();

        return services;
    }
}