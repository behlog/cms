using Microsoft.EntityFrameworkCore;
using Behlog.Core;
using Behlog.Cms.Domain;

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
            category.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    c => c.Id,
                    c => EntityStatus.FromValue<EntityStatus>(c));

            category.HasOne<ContentType>()
                .WithMany()
                .HasForeignKey(_ => _.ContentTypeId)
                .OnDelete(DeleteBehavior.SetNull);
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
                .HasForeignKey(_ => _.ContentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}