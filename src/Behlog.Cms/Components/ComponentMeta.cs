using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

/// <summary>
/// Kepping Metadata for a <see cref="Component"/>.
/// </summary>
public class ComponentMeta : MetaBase<Guid>
{
    protected ComponentMeta() : base() { }

    public static ComponentMeta New() => new ComponentMeta();

    public int Index { get; protected set; }

    public static ComponentMeta New(string metaKey) 
        => new ComponentMeta
        {
            MetaKey = metaKey
        };

    public static ComponentMeta New(string metaKey, string metaValue) 
        => new ComponentMeta
        {
            MetaKey = metaKey,
            MetaValue = metaValue
        };

    public ComponentMeta WithOwnerId(Guid categoryId) {
        OwnerId = categoryId;
        return this;
    }

    public ComponentMeta WithKey(string metaKey) {
        MetaKey = metaKey;
        return this;
    }

    public ComponentMeta WithValue(string value) {
        MetaValue = value;
        return this;
    }

    public ComponentMeta WithCategory(string category) {
        Category = category;
        return this;
    }

    public ComponentMeta WithOrderNum(int orderNum) {
        OrderNum = orderNum;
        return this;
    }

    public ComponentMeta WithDescription(string description) {
        Description = description;
        return this;
    }

    public ComponentMeta WithTitle(string title) {
        Title = title;
        return this;
    }

    public ComponentMeta WithStatus(EntityStatus status) {
        Status = status;
        return this;
    }

    public ComponentMeta WithLangId(Guid? langId) {
        LangId = langId;
        return this;
    }

    public ComponentMeta WithLangCode(string langCode) {
        LangCode = langCode;
        return this;
    }

    public ComponentMeta WithIndex(int index) {
        Index = index;
        return this;
    }

    public ComponentMeta Build() => this;
}