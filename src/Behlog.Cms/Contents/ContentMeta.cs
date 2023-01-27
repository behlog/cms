using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

public class ContentMeta : MetaBase<Guid>
{
    public ContentMeta() : base() { }

    public ContentMeta WithOwnerId(Guid categoryId)
    {
        OwnerId = categoryId;
        return this;
    }

    public static ContentMeta New()
    {
        return new ContentMeta();
    }

    public static ContentMeta New(string metaKey)
    {
        return new ContentMeta { MetaKey = metaKey };
    }

    public static ContentMeta New(string metaKey, string metaValue)
    {
        return new ContentMeta
        {
            MetaKey = metaKey,
            MetaValue = metaValue
        };
    }
    
    public ContentMeta WithKey(string metaKey)
    {
        MetaKey = metaKey;
        return this;
    }

    public ContentMeta WithValue(string value)
    {
        MetaValue = value;
        return this;
    }

    public ContentMeta WithCategory(string category)
    {
        Category = category;
        return this;
    }

    public ContentMeta WithOrderNum(int orderNum)
    {
        OrderNum = orderNum;
        return this;
    }

    public ContentMeta WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ContentMeta WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public ContentMeta WithStatus(EntityStatus status)
    {
        Status = status;
        return this;
    }

    public ContentMeta WithLangId(Guid? langId)
    {
        LangId = langId;
        return this;
    }

    public ContentMeta Build() => this;
}