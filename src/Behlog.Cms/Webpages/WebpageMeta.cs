using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

/// <summary>
/// Key/value pairs Metadata for <see cref="Webpage"/>(s).
/// </summary>
public class WebpageMeta : MetaBase<Guid>
{
    public WebpageMeta(): base() { }

    public WebpageMeta WithOwnerId(Guid webpageId) 
    {
        OwnerId = webpageId;
        return this;
    }

    public static WebpageMeta New() {
        return new WebpageMeta();
    }

    public static WebpageMeta New(string metaKey) {
        return new WebpageMeta { MetaKey = metaKey };
    }

    public static WebpageMeta New(string metaKey, string metaValue) {
        return new WebpageMeta
        {
            MetaKey = metaKey,
            MetaValue = metaValue
        };
    }

    public WebpageMeta WithKey(string metaKey) {
        MetaKey = metaKey;
        return this;
    }

    public WebpageMeta WithValue(string value) {
        MetaValue = value;
        return this;
    }

    public WebpageMeta WithCategory(string category) {
        Category = category;
        return this;
    }

    public WebpageMeta WithOrderNum(int orderNum) {
        OrderNum = orderNum;
        return this;
    }

    public WebpageMeta WithDescription(string description) {
        Description = description;
        return this;
    }

    public WebpageMeta WithTitle(string title) {
        Title = title;
        return this;
    }

    public WebpageMeta WithStatus(EntityStatus status) {
        Status = status;
        return this;
    }

    public WebpageMeta WithLangId(Guid? langId) {
        LangId = langId;
        return this;
    }

    public WebpageMeta Build() => this;
}

