using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core.Models;
using Idyfa.Core.Extensions;

namespace Behlog.Cms.Query;


public class QueryContentsFiltered : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryContentsFiltered(Guid websiteId)
    {
        WebsiteId = websiteId;
        Filter = new QueryFilter();
    }
    
    public Guid WebsiteId { get; }
    
    public QueryFilter Filter { get; set; }
    
    public Guid? LangId { get; set; }
    
    public Guid? ContentTypeId { get; set; }
    
    public string? ContentTypeName { get; set; }
    
    public string? AuthorUserId { get; set; }
    
    public ContentStatus? Status { get; set; }

    private string? _search;
    public string? Search
    {
        get => _search;
        set => _search = value?.CorrectYeKe();
    }

    private string? _title;
    public string? Title
    {
        get => _title;
        set => _title = value?.CorrectYeKe();
    }
}