using System;
using Behlog.Core;

namespace Behlog.Cms;

public class Content : AggregateRoot<Guid>
{
     
    public static Content Create() {
        var content = new Content();
        content.Id = Guid.NewGuid();
        return content;
    }

    #region Props

    public string Title { get; }
    public string Slug { get; }
    public Guid? ContentTypeId { get; }
    public string Body { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    
    #endregion
    
}