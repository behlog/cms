namespace Behlog.Cms;

public class ContentAuthor : ValueObject
{
    private ContentAuthor() { }

    private ContentAuthor(Guid contentId, string userId)
    {
        ContentId = contentId;
        UserId = userId;
    }

    private ContentAuthor(Guid contentId)
    {
        ContentId = contentId;
    }
    
    public long Id { get; private set; }
    
    public Guid ContentId { get; private set; }
    
    public string UserId { get; private set; }
    
    public string? DisplayName { get; private set; }
    
    public string? UserName { get; private set; }
    
    public bool Visible { get; private set; }

    #region Builders

    public static ContentAuthor New(Guid contentId)
    {
        var contentAuthor = new ContentAuthor(contentId);
        return contentAuthor;
    }

    public ContentAuthor WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    #endregion
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return UserId;
        yield return UserName;
        yield return DisplayName;
        yield return Visible;
    }
}