using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class Website : AggregateRoot<Guid>
{
    
    private Website() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string Keywords { get; protected set; }
    public string Url { get; protected set; }
    public string OwnerUserId { get; protected set; }
    public Guid? DefaultLangId { get; protected set; }
    public WebsiteStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string Password { get; protected set; }
    public bool IsReadOnly { get; protected set; }

    #endregion
}