using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class Language : BehlogEntity<Guid>
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

    public static async Task<Language> CreateAsync(
        CreateLanguageCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        var lang = new Language
        {
            Id = Guid.NewGuid(),
            Code = command.Code,
            Name = command.Name.Trim(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Status = EntityStatus.Enabled
        };

        var e = new LanguageCreatedEvent(
            lang.Id, lang.Title, lang.Name, lang.Code, lang.Status);
        await manager.PublishAsync(e).ConfigureAwait(false);

        return lang;
    }

    #endregion
}