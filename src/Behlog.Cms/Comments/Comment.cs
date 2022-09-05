using System;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms;

public class Comment : AggregateRoot<Guid>, IHasMetadata
{
    private readonly IDateService _dateService = new DateService();

    protected Comment()
    {
        
    }


    #region Props

    public string Title { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string Email { get; }
    public string WebUrl { get; }
    public string AuthorUserId { get; }
    public string AuthorName { get; }
    
    public DateTime CreatedDate { get; }
    public DateTime? LastUpdated { get; }
    public string CreatedByUserId { get; }
    public string LastUpdatedByUserId { get; }
    public string CreatedByIp { get; }
    public string LastUpdatedByIp { get; }
    #endregion
}