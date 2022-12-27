using Behlog.Cms.Store;
using Behlog.Cms.Query;
using Behlog.Cms.Domain;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Extensions;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentCategoryReadStore : BehlogEntityFrameworkCoreReadStore<ContentCategory, Guid>, IContentCategoryReadStore
{
    
    public ContentCategoryReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
    
    /// <inheritdoc />
    public async Task<ContentCategory> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return (await _set.Include(_ => _.Language)
            .Include(_ => _.ContentType)
            .Include(_ => _.Website)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false))!;
    }

    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindWebsiteContentCategoriesAsync(
        Guid websiteId, Guid langId, Guid? contentTypeId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.WebsiteId == websiteId)
                         .Where(_ => _.LangId == langId)
                         .AddConditionIfNotNull(contentTypeId,
                            _ => _.ContentTypeId == contentTypeId!.Value)
                         .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        QueryContentCategoryByContentType model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        return await _set.Include(_=> _.ContentType)
                         .AddConditionIfNotNull(model.LangId, 
                            _ => _.LangId == model.LangId!.Value)
                         .AddConditionIfNotNull(model.ContentTypeId, 
                            _ => _.ContentTypeId == model.ContentTypeId!.Value)
                         .AddConditionIfNotNull(model.ContentTypeName,
                            _ => _.ContentType.SystemName.ToUpper() == model.ContentTypeName!.ToUpper())
                         .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
    
    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindByParentIdAsync(
        Guid langId, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.LangId == langId && _.ParentId == parentId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}