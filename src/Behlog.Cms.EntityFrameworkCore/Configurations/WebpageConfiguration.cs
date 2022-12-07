using Behlog.Cms.Domain;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;


public static partial class EntityConfigurations
{


    public static void AddWebpageConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Webpage>(page =>
        {
            page.ToTable(WebpageTableName)
                .HasKey(_ => _.Id);

            page.Property(_ => _.Id).ValueGeneratedNever();
            page.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
            page.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
            page.Property(_ => _.Body).HasColumnType("nTEXT").IsUnicode().IsRequired(false); //TODO : It's for SQL Server, what about other RBDMSes?
            page.Property(_ => _.Summary).HasMaxLength(2000).IsUnicode().IsRequired(false);
            page.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML)
                .HasConversion<int>(
                    b => b.Id,
                    b => ContentBodyType.Find(b)
                );
            page.Property(_ => _.Status).HasDefaultValue(WebpageStatus.Created)
                .HasConversion<int>(
                    s => s.Id,
                    s => WebpageStatus.Find(s));
            page.Property(_ => _.AltTitle).HasMaxLength(1000).IsUnicode().IsRequired(false);
            page.Property(_ => _.Password).HasMaxLength(100).IsUnicode().IsRequired(false);
            page.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            page.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            page.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsRequired(false);

            page.OwnsMany(_ => _.Meta, m => {
                m.ToTable(ContentMetaTableName).HasKey("Id");
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

            page.OwnsMany(_ => _.Blocks, b => {
                b.ToTable(ContentBlockTableName).HasKey(_ => _.Id);
                b.Property(_ => _.Id).ValueGeneratedOnAdd();
                b.WithOwner().HasForeignKey(_ => _.WebpageId);
                b.HasOne<Block>().WithMany().HasForeignKey(_ => _.BlockId);
                b.Property(_ => _.Source).HasColumnType("nTEXT").IsUnicode().IsRequired();
                b.Property(_ => _.Properties).HasMaxLength(4000).IsUnicode().IsRequired(false);
                b.Property(_ => _.TextContent).HasMaxLength(4000).IsUnicode().IsRequired(false);
                b.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML)
                    .HasConversion<int>(
                        t => t.Id,
                        t => ContentBodyType.Find(t)
                    );
            });

            page.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    
}