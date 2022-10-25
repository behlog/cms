using Behlog.Core.Domain;

namespace Behlog.Core;

public class ContentType : BehlogEntity<Guid> {
    
    protected ContentType()
    {
        
    }

    #region Props
    
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
    public EntityStatus Status { get; }

    #endregion
    
    
    
}