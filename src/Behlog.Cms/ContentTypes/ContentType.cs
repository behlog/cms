using System;

namespace Behlog.Core;

public class ContentType : AggregateRoot<Guid> {


    public static ContentType Create() {
        
    }

    #region Props
    
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
    public EntityStatus Status { get; }

    #endregion
}