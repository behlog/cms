using Behlog.Core;

namespace Behlog.Cms.Models;

public class ContentCategoryResult
{

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? AltTitle { get; set; }
    public string Slug { get; set; }
    public Guid LangId { get; set; }
    public string? LangCode { get; set; }
    public Guid? ParentId { get; set; }
    public string? Description { get; set; }
    public Guid? ContentTypeId { get; set; }
    public EntityStatus Status { get; set; }
    public Guid WebsiteId { get; set; }


    #region Navigation's data

    public string? LangTitle { get; set; }
    public string? ContentTypeSystemName { get; set; }
    public string? ContentTypeTitle { get; set; }
    public string? ContentTypeSlug { get; set; }
    public string? WebsiteTitle { get; set; }
    public string? WebsiteName { get; set; }

    #endregion
}