using Behlog.Cms.Domain;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddContentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Content>(content =>
        {
            content.ToTable(ContentTableName).HasKey(_ => _.Id);

            content.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
            content.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
            content.Property(_ => _.Body).HasColumnType("nTEXT").IsUnicode().IsRequired(false); //TODO : It's for SQL Server, what about other RBDMSes?
            content.Property(_ => _.AuthorUserId).HasMaxLength(100).IsRequired();
            content.Property(_ => _.Summary).HasMaxLength(2000).IsUnicode().IsRequired(false);
            content.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML)
                .HasConversion<int>(
                    b => b.Id,
                    b => ContentBodyType.Find(b)
                );
            content.Property(_ => _.Status).HasDefaultValue(ContentStatus.Draft)
                .HasConversion<int>(
                    s => s.Id,
                    s => ContentStatus.Find(s)
                );
            content.Property(_ => _.AltTitle).HasMaxLength(1000).IsUnicode().IsRequired(false);
            content.Property(_ => _.Password).HasMaxLength(100).IsUnicode().IsRequired(false);
            content.Property(_ => _.IconName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            content.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            content.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsRequired(false);
            content.Property(_ => _.LangCode).HasMaxLength(10).IsUnicode().IsRequired(false);

            content.OwnsMany(_ => _.Meta, m => {
                m.ToTable(ContentMetaTableName);
                m.WithOwner().HasForeignKey(_ => _.OwnerId);
                m.Property<int>("Id");
                m.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                m.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                    .HasConversion<int>(
                        c => c.Id, c => EntityStatus.FromValue<EntityStatus>(c));
                m.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            });

            content.OwnsMany(_ => _.Likes, l => {
                l.ToTable(ContentLikeTableName);
                l.WithOwner().HasForeignKey(_ => _.ContentId);
                l.Property(_ => _.SessionId).HasMaxLength(256).IsRequired(false);
                l.Property(_ => _.UserId).HasMaxLength(100).IsRequired(false);
                l.Property(_ => _.IpAddress).HasMaxLength(100).IsRequired(false);
            });

            content.OwnsMany(_ => _.Files, f => {
                f.WithOwner().HasForeignKey(_ => _.ContentId);
                f.WithOwner().HasForeignKey(_ => _.FileId);
                f.ToTable(ContentFileTableName);
                f.Property(_ => _.FileName).HasMaxLength(2000).IsUnicode().IsRequired();
                f.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired(false);
                f.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
                f.HasKey(_ => new {
                    _.ContentId, 
                    _.FileId
                });
            });
            
            content.HasOne(_ => _.ContentType)
                .WithMany()
                .HasForeignKey(_ => _.ContentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            content.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}