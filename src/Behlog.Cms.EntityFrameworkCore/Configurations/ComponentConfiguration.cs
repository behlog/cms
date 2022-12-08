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

            component.OwnsMany(_ => _.Meta, m => {
                m.ToTable(ComponentMetaTableName).HasKey("Id");
                m.Property<long>("Id").ValueGeneratedOnAdd();
                m.WithOwner().HasForeignKey(_ => _.OwnerId);
                m.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                m.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                    .HasConversion<int>(
                        c => c.Id, c => EntityStatus.FromValue<EntityStatus>(c));
                m.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            });

            component.HasOne(_ => _.Website)
                    .WithMany()
                    .HasForeignKey(_ => _.WebsiteId)
                    .OnDelete(DeleteBehavior.Restrict);
        });
    }

}
