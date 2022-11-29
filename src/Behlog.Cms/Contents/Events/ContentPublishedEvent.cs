using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentPublishedEvent : BehlogDomainEvent {

    public ContentPublishedEvent(
        Guid id, DateTime publishedAt,
        string publishedByUserId, string publishedByIp)
    {
        Id = id;
        PublishedAt = publishedAt;
        PublishedByUserId = publishedByUserId;
        PublishedByIp = publishedByIp;
    }
    
    public Guid Id { get; }
    public DateTime PublishedAt { get; } 
    public string PublishedByUserId { get; }
    public string PublishedByIp { get; }
}