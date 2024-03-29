using Behlog.Cms.Contents;
using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{
    public const string Schema = "Behlog";

    public const string WebsiteTableName = nameof(Website);

    public const string WebsiteMetaTableName = nameof(WebsiteMeta);
    
    public const string ContentTableName = nameof(Content);

    public const string ContentTypeTableName = nameof(ContentType);

    public const string CommentTableName = nameof(Comment);

    public const string FileTableName = nameof(FileUpload);

    public const string LanguageTableName = nameof(Language);

    public const string ContentCategoryTableName = nameof(ContentCategory);

    public const string ContentCategoryItemTableName = nameof(ContentCategoryItem);

    public const string ContentFileTableName = nameof(ContentFile);

    public const string ContentMetaTableName = nameof(ContentMeta);

    public const string ContentLikeTableName = nameof(ContentLike);

    public const string TagTableName = nameof(Tag);

    public const string ContentTagTableName = nameof(ContentTag);
    
    public const string ComponentTableName = nameof(Component);

    public const string ComponentMetaTableName = nameof(ComponentMeta);

    public const string ComponentFileTableName = nameof(ComponentFile);
    
    public const string ContentComponentTableName = nameof(ContentComponent);

    public const string WebsiteTagTableName = nameof(WebsiteTag);

    public const string ContentTypeTagTableName = nameof(ContentTypeTag);

    public const string ContentAuthorTableName = nameof(ContentAuthor);

    public const string ContentHistoryTableName = nameof(ContentHistory);
}