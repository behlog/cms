using Behlog.Cms.Events;
using Behlog.Cms.Commands;

namespace Behlog.Cms.Domain;

public class Language : AggregateRoot<Guid>
{
    
    private Language() { }

    #region props

    public string Title { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public string? IsoCode { get; protected set; }
    public EntityStatus Status { get; protected set; }

    #endregion

    #region Builders


    public static Language Create(Guid id, CreateLanguageCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        var lang = new Language
        {
            Id = id,
            Code = command.Code,
            Name = command.Name.Trim(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Status = EntityStatus.Enabled
        };
        lang.AddCreatedEvent();
        
        return lang;
    }


    public static Language Create(CreateLanguageCommand command)
    {
        return Create(id: Guid.NewGuid(), command);
    }

    #endregion


    #region Events


    private void AddCreatedEvent()
    {
        var e = new LanguageCreatedEvent(
            Id, Title, Name, Code, Status);
        Enqueue(e);
    }

    #endregion
}