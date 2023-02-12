using Microsoft.EntityFrameworkCore;
using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;


public static partial class EntityConfigurations
{

    public static void AddWebsiteConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Website>(web =>
        {
            web.ToTable(WebsiteTableName)
                .HasKey(_ => _.Id);

            web.Property(_ => _.Id).ValueGeneratedNever();
            web.Property(_ => _.Name).HasMaxLength(256).IsUnicode().IsRequired();
            web.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
            web.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            web.Property(_ => _.Keywords).HasMaxLength(1000).IsUnicode().IsRequired(false);
            web.Property(_ => _.Url).HasMaxLength(2000).IsUnicode().IsRequired(false);
            web.Property(_ => _.OwnerUserId).HasMaxLength(100).IsRequired();
            web.Property(_ => _.Status).HasDefaultValue(WebsiteStatus.UnderConstruction);
            web.Property(_ => _.Password).HasMaxLength(100).IsUnicode().IsRequired(false);
            web.Property(_ => _.Email).HasMaxLength(2000).IsUnicode().IsRequired(false);
            web.Property(_ => _.TemplateName).HasDefaultValue("default").HasMaxLength(256).IsUnicode().IsRequired();
            web.Property(_ => _.CopyrightText).HasMaxLength(1000).IsUnicode().IsRequired(false);

            web.OwnsMany(_ => _.Meta, m =>
            {
                m.ToTable(WebsiteMetaTableName).HasKey(_=> _.Id);
                m.Property(_=> _.Id).ValueGeneratedOnAdd();
                m.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.WithOwner().HasForeignKey(_ => _.OwnerId);
                m.Property(_ => _.MetaKey).HasMaxLength(256).IsUnicode().IsRequired();
                m.Property(_ => _.MetaType).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.MetaValue).HasMaxLength(4000).IsUnicode().IsRequired(false);
                m.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled);
                m.Property(_ => _.Category).HasMaxLength(256).IsUnicode().IsRequired(false);
                m.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);
            });

            builder.Entity<WebsiteTag>(tag =>
            {
                tag.ToTable(WebsiteTagTableName).HasKey(_ => _.Id);
                tag.Property(_ => _.TagTitle).HasMaxLength(1000).IsUnicode().IsRequired();
                tag.Property(_ => _.TagSlug).HasMaxLength(1000).IsUnicode().IsRequired();

                tag.HasOne(_ => _.Website)
                    .WithMany()
                    .HasForeignKey(_ => _.WebsiteId)
                    .OnDelete(DeleteBehavior.Cascade);

                tag.HasOne(_ => _.Tag)
                    .WithMany()
                    .HasForeignKey(_ => _.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                tag.HasOne(_ => _.Language)
                    .WithMany()
                    .HasForeignKey(_ => _.LangId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        });
    }
}