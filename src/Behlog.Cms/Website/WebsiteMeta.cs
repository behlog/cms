using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

public class WebsiteMeta : MetaBase<Guid>
{
    
    protected WebsiteMeta() { }


    public static WebsiteMeta New() => new WebsiteMeta();

    public static WebsiteMeta New(string metaKey) => new WebsiteMeta
    {
        MetaKey = metaKey
    };

    public static WebsiteMeta New(string metaKey, string metaValue) => new WebsiteMeta
    {
        MetaKey = metaKey,
        MetaValue = metaValue
    };
    
    public WebsiteMeta WithOwnerId(Guid categoryId)
    {
        OwnerId = categoryId;
        return this;
    }
    
    public WebsiteMeta WithKey(string metaKey)
    {
        MetaKey = metaKey;
        return this;
    }

    public WebsiteMeta WithValue(string value)
    {
        MetaValue = value;
        return this;
    }

    public WebsiteMeta WithCategory(string category)
    {
        Category = category;
        return this;
    }

    public WebsiteMeta WithOrderNum(int orderNum)
    {
        OrderNum = orderNum;
        return this;
    }

    public WebsiteMeta WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public WebsiteMeta WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public WebsiteMeta WithStatus(EntityStatus status)
    {
        Status = status;
        return this;
    }

    public WebsiteMeta WithLangId(Guid? langId)
    {
        LangId = langId;
        return this;
    }

    public WebsiteMeta Build() => this;
}