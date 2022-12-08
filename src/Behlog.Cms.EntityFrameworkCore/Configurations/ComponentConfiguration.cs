using Behlog.Core;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;


public static partial class EntityConfigurations
{

    public static void AddComponentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Component>(component => {
            component.ToTable(ComponentTableName)
                .HasKey(_ => _.Id);

            component.Property(_ => _.Name).HasMaxLength(256).IsUnicode().IsRequired();
            component.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
            component.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired();
            component.Property(_ => _.Description).HasMaxLength(1000).IsUnicode().IsRequired(false);
            component.Property(_ => _.Author).HasMaxLength(256).IsUnicode().IsRequired(false);
            component.Property(_ => _.AuthorEmail).HasMaxLength(1000).IsUnicode().IsRequired(false);
            component.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    s => s.Id,
                    s => EntityStatus.FromValue<EntityStatus>(s));

            component.HasOne(_ => _.Website)
                    .WithMany()
                    .HasForeignKey(_ => _.WebsiteId)
                    .OnDelete(DeleteBehavior.Restrict);
        });
    }

}
