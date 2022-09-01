using System;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms;

public class Comment : AggregateRoot<Guid>
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
    
    #endregion
}