using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

public class BlockMeta : MetaBase<Guid>
{
    protected BlockMeta()
    {
    }

    public static BlockMeta New() => new BlockMeta();

    public static BlockMeta New(string metaKey) => new BlockMeta { MetaKey = metaKey };

    public static BlockMeta New(string metaKey, string metaValue)
        => new BlockMeta
        {
            MetaKey = metaKey,
            MetaValue = metaValue
        };
    
    public BlockMeta WithOwnerId(Guid categoryId)
    {
        OwnerId = categoryId;
        return this;
    }
    
    public BlockMeta WithKey(string metaKey)
    {
        MetaKey = metaKey;
        return this;
    }

    public BlockMeta WithValue(string value)
    {
        MetaValue = value;
        return this;
    }

    public BlockMeta WithCategory(string category)
    {
        Category = category;
        return this;
    }

    public BlockMeta WithOrderNum(int orderNum)
    {
        OrderNum = orderNum;
        return this;
    }

    public BlockMeta WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public BlockMeta WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public BlockMeta WithStatus(EntityStatus status)
    {
        Status = status;
        return this;
    }

    public BlockMeta WithLangId(Guid? langId)
    {
        LangId = langId;
        return this;
    }

    public BlockMeta Build() => this;
}