namespace Behlog.Cms.Query;


public class QueryContentByContentTypeAndSlug : IBehlogQuery<ContentResult>
{

    public QueryContentByContentTypeAndSlug(
        Guid websiteId, string slug, Guid? contentTypeId, string? contentTypeName, Guid? langId)
    {
        WebsiteId = websiteId;
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentNullException(nameof(slug));
        Slug = slug.CorrectYeKe().Trim();
        ContentTypeId = contentTypeId;
        ContentTypeName = contentTypeName;
        LangId = langId;
    }
    
    public Guid WebsiteId { get; }
    public Guid? ContentTypeId { get; }
    public string? ContentTypeName { get; }
    public Guid? LangId { get; }
    public string Slug { get; }
}