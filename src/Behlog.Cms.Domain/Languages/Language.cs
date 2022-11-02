using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Languages;

public class Language : BehlogEntity<Guid>
{
    
    private Language() { }

    #region props

    public string Title { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public EntityStatus Status { get; protected set; }

    #endregion

    #region Builders

    

    #endregion
}