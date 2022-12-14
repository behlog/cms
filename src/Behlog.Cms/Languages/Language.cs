using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class Language : AggregateRoot<Guid>
{
    
    private Language() { }

    #region props

    public string Title { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public string? IsoCode { get; protected set; }
    public EntityStatusEnum Status { get; protected set; }

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
            Status = EntityStatusEnum.Enabled
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