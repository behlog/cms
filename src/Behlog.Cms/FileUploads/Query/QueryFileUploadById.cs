namespace Behlog.Cms.Query;


public class QueryFileUploadById : IBehlogQuery<FileUploadResult>
{
    public QueryFileUploadById(Guid id)
    {
        id.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(FileUpload)));
        Id = id;
    }
    
    public Guid Id { get; }
}