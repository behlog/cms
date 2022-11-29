using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public partial class Template : AggregateRoot<Guid>
{
    
    private Template() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string Author { get; protected set; }
    public string Category { get; protected set; }
    public string AuthorEmail { get; protected set; }
    public string AuthorWebsite { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string SourceUrl { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    
    #endregion
}