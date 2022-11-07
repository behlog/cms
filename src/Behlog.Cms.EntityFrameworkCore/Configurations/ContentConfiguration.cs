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
            content.Property(_ => _.Body).HasColumnType("nTEXT").IsUnicode(); //TODO : It's for SQL Server, what about other RBDMSes?
            content.Property(_ => _.AuthorUserId).HasMaxLength(100).IsRequired();
            content.Property(_ => _.Summary).HasMaxLength(2000).IsUnicode();
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
            content.Property(_ => _.AltTitle).HasMaxLength(1000).IsUnicode();
            content.Property(_ => _.Password).HasMaxLength(100).IsUnicode();
            content.Property(_ => _.IconName).HasMaxLength(256).IsUnicode();
            content.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode();
            content.Property(_ => _.CreatedByUserId).HasMaxLength(100);
            content.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100);
            content.Property(_ => _.LangCode).HasMaxLength(10).IsUnicode();
            
            content.OwnsMany(_ => _.Meta)
                .ToTable(ContentMetaTableName).HasKey(_=> _.OwnerId);
            content.OwnsMany(_ => _.Meta)
                .Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
            content.OwnsMany(_ => _.Meta)
                .Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode();
            content.OwnsMany(_ => _.Meta)
                .Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    c => c.Id, c => EntityStatus.FromValue<EntityStatus>(c));
            content.OwnsMany(_ => _.Meta)
                .Property(_ => _.Category).HasMaxLength(256).IsUnicode();
            content.OwnsMany(_ => _.Meta)
                .Property(_ => _.Description).HasMaxLength(2000).IsUnicode();

            content.OwnsMany(_ => _.Likes)
                .ToTable(ContentLikeTableName).HasKey(_ => _.Id);
            content.OwnsMany(_ => _.Likes)
                .Property(_ => _.IpAddress).HasMaxLength(100).IsUnicode();
            content.OwnsMany(_ => _.Likes)
                .Property(_ => _.SessionId).HasMaxLength(1000);
            content.OwnsMany(_ => _.Likes)
                .Property(_ => _.UserId).HasMaxLength(256).IsUnicode();
            
            content.OwnsMany(_ => _.Files).ToTable(ContentFileTableName)
                .HasKey(_ => new
                {
                    _.ContentId, _.FileId
                });
            content.OwnsMany(_=> _.Files).Property(_ => _.Title).HasMaxLength(1000).IsUnicode();
            content.OwnsMany(_ => _.Files)
                .Property(_ => _.FileName).HasMaxLength(2000).IsUnicode();
            content.OwnsMany(_ => _.Files)
                .Property(_ => _.Description).HasMaxLength(2000).IsUnicode();
            
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