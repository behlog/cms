using Behlog.Cms.Contents;
using Behlog.Core;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddContentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Content>(content =>
        {
            content.ToTable(ContentTableName)
                    .HasKey(_ => _.Id);
            
            content.Property(_ => _.Id).ValueGeneratedNever();
            content.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
            content.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
            content.Property(_ => _.Body).HasColumnType("nTEXT").IsUnicode().IsRequired(false);
            content.Property(_ => _.AuthorUserId).HasMaxLength(100).IsRequired();
            content.Property(_ => _.AuthorUserDisplayName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.AuthorUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.Summary).HasMaxLength(2000).IsUnicode().IsRequired(false);
            content.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML);
            content.Property(_ => _.Status).HasDefaultValue(ContentStatusEnum.Draft);
            content.Property(_ => _.AltTitle).HasMaxLength(1000).IsUnicode().IsRequired(false);
            content.Property(_ => _.Password).HasMaxLength(100).IsUnicode().IsRequired(false);
            content.Property(_ => _.IconName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            content.Property(_ => _.CoverPhoto).HasMaxLength(2000).IsUnicode().IsRequired(false);
            content.Property(_ => _.CreatedByUserId).HasMaxLength(100).IsRequired(false);
            content.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100).IsRequired(false);
            content.Property(_ => _.LangCode).HasMaxLength(10).IsUnicode().IsRequired(false);
            content.Property(_ => _.CreatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.CreatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);
            content.Property(_ => _.LastUpdatedByUserName).HasMaxLength(256).IsUnicode().IsRequired(false);
            content.Property(_ => _.LastUpdatedByUserDisplayName).HasMaxLength(500).IsUnicode().IsRequired(false);

            content.OwnsMany(_ => _.Meta, m => {
                m.ToTable(ContentMetaTableName).HasKey(_=> _.Id);
                m.Property(_=> _.Id).ValueGeneratedOnAdd();
                m.WithOwner().HasForeignKey(_ => _.OwnerId);
                m.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                m.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
                m.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            });
            
            content.OwnsMany(_ => _.Likes, l => {
                l.ToTable(ContentLikeTableName).HasKey("Id");
                l.Property<long>("Id").ValueGeneratedOnAdd();
                l.WithOwner().HasForeignKey(_ => _.ContentId);
                l.Property(_ => _.SessionId).HasMaxLength(256).IsRequired(false);
                l.Property(_ => _.UserId).HasMaxLength(100).IsRequired(false);
                l.Property(_ => _.IpAddress).HasMaxLength(100).IsRequired(false);
            });

            content.OwnsMany(_ => _.Files, f => {
                f.ToTable(ContentFileTableName)
                    .HasKey(_ => new {
                        _.ContentId, 
                        _.FileId
                    });
                f.WithOwner().HasForeignKey(_ => _.ContentId);
                f.HasOne(_ => _.File).WithMany().HasForeignKey(_ => _.FileId);
                f.Property(_ => _.FileName).HasMaxLength(2000).IsUnicode().IsRequired();
                f.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired(false);
                f.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            });


            content.OwnsMany(_ => _.Components, comp => {
                comp.ToTable(ContentComponentTableName).HasKey(_ => _.Id);
                comp.Property(_ => _.Id).ValueGeneratedOnAdd();
                comp.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
                comp.Property(_ => _.Params).HasMaxLength(4000).IsUnicode().IsRequired(false);
                comp.Property(_ => _.ViewPath).HasMaxLength(2000).IsUnicode().IsRequired(false);
                comp.Property(_ => _.IsRtl).HasDefaultValue(false);
                comp.HasOne(_ => _.Component)
                    .WithMany()
                    .HasForeignKey(_ => _.ComponentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            content.OwnsMany(_ => _.Authors, author =>
            {
                author.ToTable(ContentAuthorTableName).HasKey(_ => _.Id);
                author.Property(_ => _.Id).ValueGeneratedOnAdd();
                author.Property(_ => _.DisplayName).HasMaxLength(256).IsUnicode().IsRequired();
                author.Property(_ => _.UserId).HasMaxLength(100).IsRequired();
                author.Property(_ => _.UserName).HasMaxLength(256).IsUnicode().IsRequired();
                author.WithOwner().HasForeignKey(_=> _.ContentId);
            });

            content.OwnsMany(_ => _.Histories, history =>
            {
                history.ToTable(ContentHistoryTableName).HasKey(_ => _.Id);
                history.Property(_ => _.Id).ValueGeneratedOnAdd();
                history.WithOwner().HasForeignKey(_ => _.ContentId);
                history.Property(_ => _.Body).HasColumnType("nTEXT").IsUnicode().IsRequired(false);
                history.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
                history.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
                history.Property(_ => _.AltTitle).HasMaxLength(1000).IsUnicode().IsRequired(false);
                history.Property(_ => _.UserId).HasMaxLength(100).IsRequired();
                history.Property(_ => _.UserDisplayName).HasMaxLength(256).IsUnicode().IsRequired(false);
                history.Property(_ => _.UserName).HasMaxLength(256).IsUnicode().IsRequired(false);
                history.Property(_ => _.Summary).HasMaxLength(2000).IsUnicode().IsRequired(false);
                history.Property(_ => _.BodyType).HasDefaultValue(ContentBodyType.HTML);
                history.Property(_ => _.IpAddress).HasMaxLength(50).IsUnicode().IsRequired(false);
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