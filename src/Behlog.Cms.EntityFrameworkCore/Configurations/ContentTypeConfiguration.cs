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
            type.ToTable(ContentTypeTableName)
                    .HasKey(_ => _.Id);

            type.Property(_ => _.Id).ValueGeneratedNever();
            type.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
            type.Property(_ => _.SystemName).HasMaxLength(50).IsUnicode().IsRequired();
            type.Property(_ => _.Slug).HasMaxLength(256).IsUnicode().IsRequired();
            type.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
            type.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);

            type.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ContentTypeTag>(tag =>
        {
            tag.ToTable(ContentTypeTagTableName).HasKey(_ => _.Id);
            tag.Property(_ => _.TagTitle).HasMaxLength(1000).IsUnicode().IsRequired();
            tag.Property(_ => _.TagSlug).HasMaxLength(1000).IsUnicode().IsRequired();

            tag.HasIndex(_ => new
            {
                _.TagId, _.ContentTypeId
            }).IsUnique();
            
            tag.HasOne(_ => _.ContentType)
                .WithMany()
                .HasForeignKey(_ => _.ContentTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            tag.HasOne(_ => _.Tag)
                .WithMany()
                .HasForeignKey(_ => _.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            tag.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}