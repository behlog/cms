using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentUpdatedEvent : BehlogDomainEvent
{

    public CommentUpdatedEvent(
        Guid id, string title, string body, ContentBodyType bodyType, string email, 
        string webUrl, string authorUserId, string authorName, string createdByUserId, 
        string lastUpdatedByUserId, string createdByIp, string lastUpdatedByIp, 
        DateTime createdDate, DateTime? lastUpdated)
    {
        Id = id;
        Title = title;
        Body = body;
        BodyType = bodyType;
        Email = email;
        WebUrl = webUrl;
        AuthorUserId = authorUserId;
        AuthorName = authorName;
        CreatedByUserId = createdByUserId;
        LastUpdatedByUserId = lastUpdatedByUserId;
        CreatedByIp = createdByIp;
        LastUpdatedByIp = lastUpdatedByIp;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
    }
    
    public Guid Id { get; set; }   
    public string Title { get; set; }
    public string Body { get; set; }
    public ContentBodyType BodyType { get; set; }
    public string Email { get; set; }
    public string WebUrl { get; set; }
    public string AuthorUserId { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string CreatedByUserId { get; set; }
    public string LastUpdatedByUserId { get; set; }
    public string CreatedByIp { get; set; }
    public string LastUpdatedByIp { get; set; }
}