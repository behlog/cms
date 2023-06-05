using Microsoft.EntityFrameworkCore;
using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{
    
    public static void AddTagConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Tag>(tag =>
        {
            tag.ToTable(TagTableName).HasKey(_ => _.Id);

            tag.Property(_ => _.Id).ValueGeneratedNever();
            tag.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
            tag.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
            tag.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
            tag.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            tag.Property(_ => _.CreatedByIp).HasMaxLength(50).IsRequired(false);
            tag.Property(_ => _.CreatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            tag.Property(_ => _.CreatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);
            tag.Property(_ => _.LastUpdatedByIp).HasMaxLength(50).IsRequired(false);
            tag.Property(_ => _.LastUpdatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            tag.Property(_ => _.LastUpdatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);

            tag.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ContentTag>(tag =>
        {
            tag.ToTable(ContentTagTableName)
                .HasKey(_ => new
                {
                    _.ContentId,
                    _.TagId
                });
            
            tag.HasOne(_ => _.Content)
                .WithMany(_=> _.Tags)
                .HasForeignKey(_ => _.ContentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            tag.HasOne(_ => _.Tag)
                .WithMany()
                .HasForeignKey(_ => _.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}