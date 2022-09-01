using System;

namespace Behlog.Cms;

public class Content 
{
     

    public static Content Create() {

        return new();
    }

    #region Props

    public string Title { get; }
    public string Slug { get; }
    public Guid? ContentTypeId { get; }
    public string Body { get; }
    public string AuthorUserId { get; }

    #endregion
    
}