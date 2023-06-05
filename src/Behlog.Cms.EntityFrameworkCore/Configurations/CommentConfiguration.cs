using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddCommentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Comment>(comment =>
        {
            comment.ToTable(CommentTableName).HasKey(_ => _.Id);
            
            comment.Property(_ => _.Id).ValueGeneratedNever();
            comment.Property(_ => _.Title).HasMaxLength(500).IsUnicode().IsRequired(false);
            comment.Property(_ => _.Body).HasMaxLength(4000).IsUnicode().IsRequired();
            comment.Property(_ => _.Email).HasMaxLength(1000).IsUnicode().IsRequired(false);
            comment.Property(_ => _.WebUrl).HasMaxLength(2000).IsUnicode().IsRequired(false);
            comment.Property(_ => _.AuthorUserId).HasMaxLength(100).IsRequired(false);
            comment.Property(_ => _.AuthorName).HasMaxLength(256).IsUnicode().IsRequired(false);
            comment.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            comment.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsRequired(false);
            comment.Property(_ => _.CreatedByIp).HasMaxLength(50).IsRequired(false);
            comment.Property(_ => _.LastUpdatedByIp).HasMaxLength(50).IsRequired(false);
            comment.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML);
            comment.Property(_ => _.Status).HasDefaultValue(CommentStatusEnum.Created);
            comment.Property(_ => _.CreatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            comment.Property(_ => _.CreatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);
            comment.Property(_ => _.LastUpdatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            comment.Property(_ => _.LastUpdatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);

            comment.HasOne(_ => _.Content)
                .WithMany()
                .HasForeignKey(_ => _.ContentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}