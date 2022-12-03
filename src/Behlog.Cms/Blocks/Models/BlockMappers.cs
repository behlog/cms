using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;

public static class BlockMappers
{


    public static MetaResult MapToResult(this BlockMeta meta)
    {
        return new MetaResult
        {
            Category = meta.Category,
            Description = meta.Description,
            Status = meta.Status,
            Title = meta.Title,
            LangCode = meta.LangCode,
            LangId = meta.LangId,
            MetaKey = meta.MetaKey,
            MetaType = meta.MetaType,
            MetaValue = meta.MetaValue,
            OrderNum = meta.OrderNum,
            OwnerId = meta.OwnerId
        };
    }
}