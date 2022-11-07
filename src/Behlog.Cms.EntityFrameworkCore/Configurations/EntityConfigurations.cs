using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{
    public const string Schema = "Behlog";
    
    public const string ContentTableName = nameof(Content);

    public const string ContentTypeTableName = nameof(ContentType);

    public const string CommentTableName = nameof(Comment);

    public const string FileTableName = nameof(FileUpload);

    public const string ContentCategoryTableName = nameof(ContentCategory);

    public const string ContentCategoryItemTableName = nameof(ContentCategoryItem);

    public const string ContentFileTableName = nameof(ContentFile);

    public const string ContentMetaTableName = nameof(ContentMeta);

    public const string ContentLikeTableName = nameof(ContentLike);
}