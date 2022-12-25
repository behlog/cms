using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

public class ContentCategoryMeta : MetaBase<Guid>
{
    protected ContentCategoryMeta() : base()
    {
    }

    public static ContentCategoryMeta New()
    {
        return new ContentCategoryMeta();
    }

    public static ContentCategoryMeta New(string meatKey)
    {
        var result = new ContentCategoryMeta
        {
            MetaKey = meatKey
        };
        return result;
    }

    public ContentCategoryMeta WithOwnerId(Guid categoryId)
    {
        OwnerId = categoryId;
        return this;
    }
    
    public ContentCategoryMeta WithKey(string metaKey)
    {
        MetaKey = metaKey;
        return this;
    }

    public ContentCategoryMeta WithValue(string value)
    {
        MetaValue = value;
        return this;
    }

    public ContentCategoryMeta WithCategory(string category)
    {
        Category = category;
        return this;
    }

    public ContentCategoryMeta WithOrderNum(int orderNum)
    {
        OrderNum = orderNum;
        return this;
    }

    public ContentCategoryMeta WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ContentCategoryMeta WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public ContentCategoryMeta WithStatus(EntityStatusEnum status)
    {
        Status = status;
        return this;
    }

    public ContentCategoryMeta WithLangId(Guid? langId)
    {
        LangId = langId;
        return this;
    }

    public ContentCategoryMeta Build() => this;
}