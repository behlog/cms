using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain.Models;

public class CommentResult
{
    public Guid Id { get; set; }   
    public string Title { get; set; }
    public string Body { get; set; }
    public ContentBodyType BodyType { get; set; }
    public string Email { get; set; }
    public string WebUrl { get; set; }
    public string AuthorUserId { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string CreatedByUserId { get; set; }
    public string LastUpdatedByUserId { get; set; }
    public string CreatedByIp { get; set; }
    public string LastUpdatedByIp { get; set; }
}

public class CommentCommandResult : CommandResult<CommentResult>
{

    public CommentCommandResult(CommentResult result) : base(result)
    {
    }
}