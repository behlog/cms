using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class ContentLike : ValueObject
{
    private ContentLike()
    {
    }

    private ContentLike(Guid contentId)
    {
        ContentId = contentId;
        CreatedDate = DateTime.UtcNow;
    }
    
    public long Id { get; protected set; }
    
    public Guid ContentId { get; protected set; }
    
    public string? UserId { get; protected set; }
    
    public string? SessionId { get; protected set; }
    
    public string? IpAddress { get; protected set; }
    
    public DateTime CreatedDate { get; protected set; }

    #region Builders

    public static ContentLike New(Guid contentId)
    {
        var like = new ContentLike(contentId);
        return like;
    }

    public ContentLike WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public ContentLike WithSessionId(string sessionId)
    {
        SessionId = sessionId;
        return this;
    }

    public ContentLike WithIpAddress(string ipAddress)
    {
        IpAddress = ipAddress;
        return this;
    }
    
    #endregion
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return UserId;
        yield return SessionId;
        yield return IpAddress;
        yield return CreatedDate;
    }
}