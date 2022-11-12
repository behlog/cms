using Behlog.Cms.Commands;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class Website : AggregateRoot<Guid>
{
    
    private Website() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string Keywords { get; protected set; }
    public string Url { get; protected set; }
    public string OwnerUserId { get; protected set; }
    public Guid? DefaultLangId { get; protected set; }
    public WebsiteStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string Password { get; protected set; }
    public bool IsReadOnly { get; protected set; }
    public string Email { get; protected set; }
    public string CopyrightText { get; protected set; }
    
    #endregion



    #region Builders


    public static Website Create(CreateWebsiteCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = new Website
        {
            Id = Guid.NewGuid(),
            Name = command.Name?.Trim().CorrectYeKe(),
            Email = command.Email?.Trim(),
            Title = command.Title?.Trim().CorrectYeKe(),
            Password = command.Password,
            Status = WebsiteStatus.Online,
            Description = command.Description,
            Keywords = command.Keywords,
            Url = command.Url,
            CopyrightText = command.CopyrightText,
            CreatedDate = DateTime.UtcNow,
            DefaultLangId = command.DefaultLangId,
            IsReadOnly = command.IsReadOnly,
            OwnerUserId = "", //TODO : Set UserId
        };
        
        
        //Add WebsiteCreatedEvent
        return website;
    }

    #endregion
}