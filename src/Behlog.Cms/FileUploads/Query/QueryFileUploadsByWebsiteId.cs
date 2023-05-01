namespace Behlog.Cms.Query;


public class QueryFileUploadsByWebsiteId : IBehlogQuery<QueryResult<FileUploadResult>>
{
    public QueryFileUploadsByWebsiteId(Guid websiteId)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        WebsiteId = websiteId;
        Options = QueryOptions.New()
            .WillOrderBy("id").WithPageNumber(1)
            .WithPageSize(10).WillOrderDesc();
    }

    public QueryFileUploadsByWebsiteId(Guid websiteId, QueryOptions options)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        WebsiteId = websiteId;

        Options = options;
        if (Options is null)
        {
            Options = QueryOptions.New()
                .WillOrderBy("id").WithPageNumber(1)
                .WithPageSize(10).WillOrderDesc();
        }
    }

    public QueryFileUploadsByWebsiteId WithStatus(FileUploadStatus status)
    {
        Status = status;
        return this;
    }

    public QueryFileUploadsByWebsiteId WithFileType(FileTypeEnum fileType)
    {
        FileType = fileType;
        return this;
    }
    
    public Guid WebsiteId { get; }
    
    public FileUploadStatus? Status { get; private set; }
    
    public FileTypeEnum? FileType { get; private set; }
    
    public QueryOptions Options { get; set; }
}