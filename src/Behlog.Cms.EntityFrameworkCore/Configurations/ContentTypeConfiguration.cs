using Microsoft.EntityFrameworkCore;
using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddContentTypeConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ContentType>(type =>
        {
            type.ToTable(ContentTypeTableName).HasKey(_ => _.Id);

            type.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
            type.Property(_ => _.SystemName).HasMaxLength(50).IsUnicode().IsRequired();
            type.Property(_ => _.Slug).HasMaxLength(256).IsUnicode().IsRequired();
            type.Property(_ => _.Lang).HasMaxLength(20).IsUnicode().IsRequired();
            type.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    c => c.Id,
                    c => EntityStatus.FromValue<EntityStatus>(c));
            type.Property(_ => _.Description).HasMaxLength(2000).IsUnicode();
            
        });
    }
}