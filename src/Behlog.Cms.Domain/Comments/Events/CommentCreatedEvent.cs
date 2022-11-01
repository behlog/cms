using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentCreatedEvent : BehlogDomainEvent
{

    public CommentCreatedEvent(
        Guid id, string title, string body, ContentBodyType bodyType,
        string email, string webUrl, string authorUserId, string authorName, 
        string createdByUserId, string createdByIp, DateTime createdDate)
    {
        Title = title;
        Body = body;
        BodyType = bodyType;
        Email = email;
        WebUrl = webUrl;
        AuthorUserId = authorUserId;
        AuthorName = authorName;
        CreatedByUserId = createdByUserId;
        CreatedByIp = createdByIp;
        CreatedDate = createdDate;
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
    public string CreatedByUserId { get; set; }
    public string CreatedByIp { get; set; }

}