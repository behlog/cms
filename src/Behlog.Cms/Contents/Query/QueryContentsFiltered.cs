namespace Behlog.Cms.Query;


public class QueryContentsFiltered : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryContentsFiltered(Guid websiteId)
    {
        WebsiteId = websiteId;
        Options = QueryOptions.New()
            .WillOrderBy("id")
            .WithPageSize(1)
            .WithPageSize(10)
            .WillOrderDesc();
    }
    
    public Guid WebsiteId { get; }
    
    public QueryOptions Options { get; set; }
    
    public Guid? LangId { get; set; }
    
    public Guid? ContentTypeId { get; set; }
    
    public string? ContentTypeName { get; set; }
    
    public string? AuthorUserId { get; set; }
    
    public ContentStatusEnum? Status { get; set; }

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