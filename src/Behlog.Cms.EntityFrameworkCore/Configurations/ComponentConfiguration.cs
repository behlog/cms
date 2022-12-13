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
            component.Property(_ => _.ComponentType).HasMaxLength(50).IsUnicode().IsRequired();
            component.Property(_ => _.Description).HasMaxLength(1000).IsUnicode().IsRequired(false);
            component.Property(_ => _.Attributes).HasColumnType("nTEXT").IsRequired(false);
            component.Property(_ => _.Author).HasMaxLength(256).IsUnicode().IsRequired(false);
            component.Property(_ => _.AuthorEmail).HasMaxLength(1000).IsUnicode().IsRequired(false);
            component.Property(_ => _.Keywords).HasMaxLength(256).IsUnicode().IsRequired(false);
            component.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            component.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    s => s.Id,
                    s => EntityStatus.FromValue<EntityStatus>(s));
            component.Property(_ => _.CreatedByIp).HasMaxLength(50).IsUnicode().IsRequired(false);
            component.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByIp).HasMaxLength(50).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsUnicode().IsRequired(false);
            

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
                m.Property(_ => _.Index).HasColumnName("IndexNumber").IsRequired();
            });

            component.HasOne(_ => _.Website)
                    .WithMany()
                    .HasForeignKey(_ => _.WebsiteId)
                    .OnDelete(DeleteBehavior.Restrict);

            component.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

}
