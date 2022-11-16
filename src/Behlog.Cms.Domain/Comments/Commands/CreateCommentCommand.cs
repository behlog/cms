using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Domain.Models;

namespace Behlog.Cms.Commands;


public class CreateCommentCommand : IBehlogCommand<CommentCommandResult>
{
    
    public Guid ContentId { get; }
    public string Title { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string Email { get; }
    public string WebUrl { get; }
    public string AuthorName { get; }
}