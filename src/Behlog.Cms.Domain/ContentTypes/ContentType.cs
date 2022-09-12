using System;
using iman.Domain;

namespace Behlog.Core;

public class ContentType : AggregateRoot<Guid> {
    
    protected ContentType(IMediator mediator) : base(mediator)
    {
        
    }

    #region Props
    
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
    public EntityStatus Status { get; }

    #endregion
    
    
    
}