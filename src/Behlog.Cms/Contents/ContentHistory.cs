namespace Behlog.Cms.Contents;

public class ContentHistory : ValueObject
{
    private ContentHistory() { }

    private ContentHistory(Guid contentId)
    {
        ContentId = contentId;
    }
    
    public long Id { get; private set; }
    public Guid ContentId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string Title { get; private set; }
    public ContentBodyType BodyType { get; private set; }
    public string? Body { get; private set; }
    public string? Summary { get; private set; }
    public string UserId { get; private set; }
    public string? UserDisplayName { get; private set; }
    public string? UserName { get; private set; }
    public string? IpAddress { get; private set; }

    #region Builders
    
    

    #endregion
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return ContentId;
        yield return CreatedDate;
        yield return Body;
        yield return BodyType;
        yield return Summary;
        yield return UserId;
    }
}