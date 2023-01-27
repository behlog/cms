using Behlog.Core;
using Behlog.Cms.Commands;

namespace Behlog.Cms.Domain;


internal static class ContentCategoryExtensions
{

    public static ICollection<ContentMeta> Convert(
        this IReadOnlyCollection<MetaCommand>? commands, Guid contentId)
    {
        return commands?.ToList()
            .Select(meta => ContentMeta
                .New(meta.MetaKey, meta.MetaValue!)
                .WithCategory(meta.Category!)
                .WithStatus(meta.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled)
                .WithTitle(meta.Title)
                .WithDescription(meta.Description!)
                .WithLangId(meta.LangId)
                .WithOrderNum(meta.OrderNum)
                .WithOwnerId(contentId)
                .Build()
            ).ToList()!;
    }



    public static ICollection<ContentCategoryItem> Convert(
        this IReadOnlyCollection<Guid>? categories, Guid contentId)
    {
        return categories?
            .ToList()
            .Select(categoryId => new ContentCategoryItem(contentId, categoryId))
            .ToList()!;
    }


    public static ICollection<ContentFile> Convert(
        this IReadOnlyCollection<ContentFileCommand>? files, Guid contentId)
    {
        return files?.ToList()
            .Select(file => ContentFile.New(contentId, file.FileId)
                .WithFileName(file.FileName)
                .WithTitle(file.Title)
                .WithDescription(file.Description))
            .ToList()!;
    }
}