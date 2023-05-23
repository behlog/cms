namespace Behlog.Cms.Contents;

/// <summary>
/// Keep tracks of <see cref="Content"/> changes.
/// When Content's body, summary or title changes.
/// </summary>
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
    public string? AltTitle { get; private set; }
    public string Slug { get; private set; }
    public ContentBodyType BodyType { get; private set; }
    public string? Body { get; private set; }
    public string? Summary { get; private set; }
    public string UserId { get; private set; }
    public string? UserDisplayName { get; private set; }
    public string? UserName { get; private set; }
    public string? IpAddress { get; private set; }

    #region Builders

    public static ContentHistory New(Guid contentId)
    {
        var history = new ContentHistory(contentId);
        history.CreatedDate = DateTime.UtcNow;
        return history;
    }

    public ContentHistory WithTitle(string title)
    {
        Title = title?.CorrectYeKe();
        return this;
    }

    public ContentHistory WithAltTitle(string altTitle)
    {
        AltTitle = altTitle?.CorrectYeKe();
        return this;
    }

    public ContentHistory WithSlug(string slug)
    {
        Slug = slug?.CorrectYeKe();
        return this;
    }

    public ContentHistory WithBody(string body, ContentBodyType bodyType = ContentBodyType.HTML)
    {
        Body = body?.CorrectYeKe();
        BodyType = bodyType;
        return this;
    }

    public ContentHistory WithUser(string userId, string displayName, string userName)
    {
        UserId = userId;
        UserDisplayName = displayName?.CorrectYeKe();
        UserName = userName?.CorrectYeKe();
        return this;
    }

    public ContentHistory WithBodyType(ContentBodyType bodyType)
    {
        BodyType = bodyType;
        return this;
    }

    public ContentHistory WithSummary(string summary)
    {
        Summary = summary?.CorrectYeKe();
        return this;
    }

    public ContentHistory WithIpAddress(string ipAddress)
    {
        IpAddress = ipAddress;
        return this;
    }

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
        yield return IpAddress;
    }
}