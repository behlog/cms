using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;


public partial class Webpage : AggregateRoot<Guid>, IHasMetadata
{
    
    private Webpage() { }


    #region props

    public Guid WebsiteId { get; protected set; }
    public string Title { get; protected set; }
    public string? AltTitle { get; protected set; }
    public string Slug { get; protected set; }
    public string Name { get; protected set; }
    public Guid LangId { get; protected set; }
    public string? Body { get; protected set; }
    public string? Summary { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string? HtmlFileName { get; protected set; }
    public bool IsHomePage { get; protected set; }
    public string? ViewPath { get; protected set; }
    public DateTime? PublishDate { get; protected set; }
    public string? Password { get; protected set; }
    public WebpageStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    
    #endregion

    
}