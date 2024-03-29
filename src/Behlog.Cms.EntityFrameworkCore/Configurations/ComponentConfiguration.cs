﻿using Behlog.Core;
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
            component.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
            component.Property(_ => _.CreatedByIp).HasMaxLength(50).IsUnicode().IsRequired(false);
            component.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByIp).HasMaxLength(50).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsUnicode().IsRequired(false);
            component.Property(_ => _.CreatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            component.Property(_ => _.CreatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            component.Property(_ => _.LastUpdatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);

            component.OwnsMany(_ => _.Meta, m => {
                m.ToTable(ComponentMetaTableName).HasKey(_=> _.Id);
                m.Property(_=> _.Id).ValueGeneratedOnAdd();
                m.WithOwner().HasForeignKey(_ => _.OwnerId);
                m.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                m.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
                m.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Index).HasColumnName("IndexNumber").IsRequired();
                //m.Metadata.DependentToPrincipal.SetField("_meta");
            });

            component.OwnsMany(_ => _.Files, f => {
                f.ToTable(ComponentFileTableName)
                    .HasKey(_ => new {
                        _.ComponentId, 
                        _.FileId
                    });
                f.WithOwner().HasForeignKey(_ => _.ComponentId);
                f.HasOne(_ => _.File).WithMany().HasForeignKey(_ => _.FileId);
                f.Property(_ => _.FileName).HasMaxLength(2000).IsUnicode().IsRequired();
                f.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired(false);
                f.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
                //f.Metadata.DependentToPrincipal.SetField("_files");
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
