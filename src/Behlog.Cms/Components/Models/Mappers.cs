using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

internal static class ComponentMappers
{

    public static MetaResult ToResult(this ComponentMeta meta)
    {
        meta.ThrowExceptionIfArgumentIsNull(nameof(meta));

        return new MetaResult(
            meta.OwnerId, meta.Title, meta.MetaKey, meta.MetaValue, meta.MetaType,
            meta.Status, meta.LangId, meta.Description, meta.Category, meta.OrderNum);
    }


    public static ComponentResult ToResult(this Component component)
    {
        component.ThrowExceptionIfArgumentIsNull(nameof(component));

        var result = ComponentResult.Create(
            component.Id, component.WebsiteId, component.LangId, component.Name, component.Title,
            component.ComponentType, component.Category, component.Attributes, component.Description,
            component.Status, component.Author, component.AuthorEmail, component.ParentId, component.IsRtl,
            component.Keywords, component.ViewPath, component.CreatedDate, component.LastUpdated,
            component.CreatedByUserId, component.CreatedByIp, component.LastUpdatedByUserId, component.LastUpdatedByIp
            );

        if (component.Meta != null && component.Meta.Any())
        {
            result.Meta = component.Meta.Select(_ => _.ToResult()).ToList();
        }

        return result;
    }
}