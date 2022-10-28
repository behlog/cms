using Behlog.Core;

namespace Behlog.Cms.Domain.Models;

public class CommentResult : BehlogResult
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