using Behlog.Cms.Domain;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations 
{


    public static void AddBlockConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Block>(block =>
        {
            block.ToTable(BlockTableName).HasKey(_ => _.Id);
            
            block.Property(_ => _.Id).ValueGeneratedNever();
            block.Property(_ => _.Name).HasMaxLength(256).IsUnicode().IsRequired();
            block.Property(_ => _.Title).HasMaxLength(500).IsUnicode().IsRequired();
            block.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
            block.Property(_ => _.Author).HasMaxLength(256).IsUnicode().IsRequired(false);
            block.Property(_ => _.AuthorEmail).HasMaxLength(1000).IsUnicode().IsRequired(false);
            block.Property(_ => _.BlockType).HasMaxLength(256).IsUnicode().IsRequired();
            block.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            block.Property(_ => _.IconName).HasMaxLength(100).IsUnicode().IsRequired(false);
            block.Property(_ => _.CoverPhoto).HasMaxLength(2000).IsUnicode().IsRequired(false);
            block.Property(_ => _.Template).HasColumnType("nTEXT").IsUnicode().IsRequired();
            block.Property(_ => _.Attributes).HasMaxLength(4000).IsUnicode().IsRequired(false);
            block.Property(_ => _.Example).HasColumnType("nTEXT").IsUnicode().IsRequired(false);
            block.Property(_ => _.Keywords).HasMaxLength(500).IsUnicode().IsRequired(false);
            block.Property(_ => _.ViewPath).HasMaxLength(4000).IsUnicode().IsRequired(false);
            block.Property(_ => _.Status).HasDefaultValue(BlockStatus.Enabled)
                .HasConversion<int>(
                    s => s.Id,
                    s => BlockStatus.FromValue<BlockStatus>(s));
            block.Property(_ => _.CreatedByIp).HasMaxLength(50).IsRequired(false);
            block.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            
            block.OwnsMany(_ => _.Meta, meta =>
            {
                meta.ToTable(BlockMetaTableName).HasKey("Id");
                meta.Property<long>("Id").ValueGeneratedOnAdd();
                meta.WithOwner().HasForeignKey(_ => _.OwnerId);
                meta.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired(false);
                meta.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                meta.Property(_ => _.MetaType).HasMaxLength(100).IsUnicode().IsRequired(false);
                meta.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                meta.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                meta.Property(_ => _.Description).HasMaxLength(2000).IsUnicode();
                meta.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                    .HasConversion<int>(
                        c => c.Id, c => EntityStatus.FromValue<EntityStatus>(c));
            });

            block.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}