using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Domain;

internal static class ContentCategoryExts
{

    public static ICollection<ContentMeta>? Convert(
        this IReadOnlyCollection<MetaCommand>? commands, Guid contentId)
    {
        return commands?.ToList()
            .Select(meta => ContentMeta
                .New(meta.MetaKey, meta.MetaValue)
                .WithCategory(meta.Category!)
                .WithStatus(meta.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled)
                .WithTitle(meta.Title)
                .WithDescription(meta.Description!)
                .WithLangId(meta.LangId)
                .WithOrderNum(meta.OrderNum)
                .WithOwnerId(contentId)
                .Build()
            ).ToList();
    }



    public static ICollection<ContentCategoryItem> Convert(
        this IReadOnlyCollection<Guid>? categories, Guid contentId)
    {
        return categories?
            .ToList()
            .Select(categoryId => new ContentCategoryItem(contentId, categoryId))
            .ToList()!;
    }

}