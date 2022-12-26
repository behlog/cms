using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class CreateCommentCommand : IBehlogCommand<CommandResult<CommentResult>>
{
    
    public Guid ContentId { get; }
    public string Title { get; }
    public string Body { get; }
    public ContentBodyTypeEnum BodyType { get; }
    public string Email { get; }
    public string WebUrl { get; }
    public string AuthorName { get; }
}