using System;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms;

public class Comment : AggregateRoot<Guid>, IHasMetadata
{
    private readonly IDateService _dateService = new DateService();

    protected Comment(CreateCommentArg args) : base()
    {
        if(args is null) throw new ArgumentNullException(nameof(args));

        Id = Guid.NewGuid();
        Title = args.Title;
        Body = args.Body;
        BodyType = args.BodyType;
        WebUrl = args.WebUrl;
        AuthorName = args.AuthorName;
        // AuthorUserId = //TODO : save logged-in UserId
        CreatedDate = DateTime.UtcNow; //TODO : get from dateservice

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

    public static Comment Create(CreateCommentArg args) 
    {
        var comment = new Comment(args);

        return comment;
    }
}