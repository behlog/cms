using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;

public static class BlockMappers
{


    public static MetaResult MapToResult(this BlockMeta meta)
    {
        return new MetaResult(meta.OwnerId, meta.Title, meta.MetaKey, meta.MetaValue,
            meta.MetaType, meta.Status, meta.LangId, meta.Description, meta.Category, meta.OrderNum);
    }
}