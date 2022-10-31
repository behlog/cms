using System;
using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class Comment : BehlogEntity<Guid>, IHasMetadata
{
    // private readonly IDateService _dateService = new DateService();

    protected Comment(CreateCommentArg args)
    {
        if(args is null) throw new ArgumentNullException(nameof(args));

        // Id = Guid.NewGuid();
        Title = args.Title;
        Body = args.Body;
        BodyType = args.BodyType;
        WebUrl = args.WebUrl;
        Email = args.Email;
        AuthorName = args.AuthorName;
        // AuthorUserId = //TODO : save logged-in UserId
        CreatedDate = DateTime.UtcNow; //TODO : get from dateservice

    }

    #region Props

    public string Title { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string Email { get; protected set; }
    public string WebUrl { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string AuthorName { get; protected set; }
    public Guid ContentId { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Content Content { get; protected set; }

    #endregion

    public static Comment Create(CreateCommentArg args) 
    {
        var comment = new Comment(args);

        return comment;
    }
}