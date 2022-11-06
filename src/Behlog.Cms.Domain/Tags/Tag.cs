using System.Data;
using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class Tag : AggregateRoot<Guid>
{
    private Tag() { }
    

    #region props

    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid LangId { get; protected set; }
    public string LangCode { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string CreatedByUserIp { get; protected set; }

    #endregion

    #region Builders

    public static Tag Create(CreateTagCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var tag = new Tag();
        tag.Id = Guid.NewGuid();
        tag.Title = command.Title?.CorrectYeKe().Trim()!;
        tag.Slug = tag.Title?.MakeSlug()!;
        tag.Status = EntityStatus.Enabled;
        tag.CreatedDate = DateTime.UtcNow;
        tag.CreatedByUserId = null; //read from UserContext
        tag.CreatedByUserIp = null; //TODO : read from HttpContext
        tag.LangId = command.LangId;
        tag.LangCode = ""; //TODO :

        //TODO : add CreatedEvent
        return tag;
    }
    
    #endregion
}