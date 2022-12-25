using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class LanguageCreatedEvent : BehlogDomainEvent
{
    public LanguageCreatedEvent(
        Guid id, string title, string name, 
        string code, EntityStatusEnum status)
    {
        Id = id;
        Title = title;
        Name = name;
        Code = code;
        Status = status;
    }
    
    public Guid Id { get; protected set; }
    public string Title { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public EntityStatusEnum Status { get; protected set; }
}