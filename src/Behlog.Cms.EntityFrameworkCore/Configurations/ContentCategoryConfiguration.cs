using Behlog.Core;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;


public static partial class EntityConfigurations
{

    public static void AddContentCategoryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ContentCategory>(category =>
        {
            category.ToTable(ContentCategoryTableName).HasKey(_ => _.Id);
            
            category.Property(_ => _.Id).ValueGeneratedNever();
            category.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
            category.Property(_ => _.AltTitle).HasMaxLength(500).IsUnicode().IsRequired(false);
            category.Property(_ => _.Slug).HasMaxLength(256).IsUnicode().IsRequired();
            category.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            category.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
            category.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            category.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsRequired(false);
            category.Property(_ => _.CreatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            category.Property(_ => _.CreatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);
            category.Property(_ => _.LastUpdatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            category.Property(_ => _.LastUpdatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);

            category.HasOne(_=> _.ContentType)
                .WithMany()
                .HasForeignKey(_ => _.ContentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            category.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);

            category.HasOne(_ => _.Website)
                .WithMany()
                .HasForeignKey(_ => _.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ContentCategoryItem>(item =>
        {
            item.ToTable(ContentCategoryItemTableName)
                .HasKey(_ => new { _.ContentId, _.CategoryId });

            item.HasOne(_ => _.Content)
                .WithMany(_ => _.Categories)
                .HasForeignKey(_ => _.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            item.HasOne(_ => _.Category)
                .WithMany(_ => _.Contents)
                .HasForeignKey(_ => _.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}