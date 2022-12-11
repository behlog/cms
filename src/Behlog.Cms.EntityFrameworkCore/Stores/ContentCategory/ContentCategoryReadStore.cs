using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentCategoryReadStore : BehlogReadStore<ContentCategory, Guid>, IContentCategoryReadStore
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
        var query = _set.Where(_ => _.WebsiteId == websiteId)
                        .Where(_=> _.LangId == langId);

        if (contentTypeId.HasValue)
            query = query.Where(_ => _.ContentTypeId == contentTypeId.Value);
        
        return await query.Where(_ => _.WebsiteId == websiteId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        Guid? langId, Guid? contentTypeId, CancellationToken cancellationToken = default)
    {
        var query = _set.AsQueryable();
        if(langId.HasValue) {
            query = query.Where(_ => _.LangId == langId.Value);
        }
        if(contentTypeId.HasValue) {
            query = query.Where(_ => _.ContentTypeId == contentTypeId);
        }

        return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<ContentCategory>> FindByParentIdAsync(
        Guid langId, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.LangId == langId && _.ParentId == parentId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}