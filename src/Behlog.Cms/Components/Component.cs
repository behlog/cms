﻿using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

/// <summary>
/// Represents Behlog's WebComponents.
/// </summary>
public class Component : AggregateRoot<Guid>, IHasMetadata
{
    private Component() : base() { }


    #region props
    public Guid WebsiteId { get; protected set; }
    public Guid LangId { get; protected set; }
    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string ComponentType { get; protected set; }
    public string Category { get; protected set; }
    public string? Attributes { get; protected set; }
    public string? Description { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public string? Author { get; protected set; }
    public string? AuthorEmail { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public bool IsRtl { get; protected set; }
    public string? Keywords { get; protected set; }
    public string? ViewPath { get; protected set; }

    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion
    
    #region Navigations

    public Website Website { get; protected set; }
    public Language Language { get; protected set; }

    public IEnumerable<ComponentMeta> Meta { get; protected set; }

    #endregion
}
