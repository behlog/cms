using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class Block : AggregateRoot<Guid>, IHasMetadata
{
    private Block() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string BlockType { get; protected set; }
    public string Category { get; protected set; }
    public string Author { get; protected set; }
    public string AuthorEmail { get; protected set; }
    public string Description { get; protected set; }
    public string IconName { get; protected set; }
    public string CoverPhoto { get; protected set; }
    public string Template { get; protected set; }
    public string Attributes { get; protected set; }
    public string Example { get; protected set; }
    public bool IsRtl { get; protected set; }
    public Guid LangId { get; protected set; }
    public string Keywords { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public BlockStatus Status { get; protected set; }
    public string ViewPath { get; protected set; }
    
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Language Language { get; protected set; }

    #endregion
}