using System.Runtime.InteropServices.ComTypes;
using Behlog.Cms.Domain;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{


    public static void AddTagConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Tag>(tag =>
        {
            tag.ToTable(TagTableName).HasKey(_ => _.Id);

            tag.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired();
            tag.Property(_ => _.Slug).HasMaxLength(1000).IsUnicode().IsRequired();
            tag.Property(_ => _.Status).HasDefaultValue(EntityStatus.Enabled)
                .HasConversion<int>(
                    s => s.Id,
                    s => EntityStatus.FromValue<EntityStatus>(s));
            tag.Property(_ => _.CreatedByUserId).HasMaxLength(100);
            tag.Property(_ => _.CreatedByIp).HasMaxLength(50);

            tag.HasOne(_ => _.Language)
                .WithMany()
                .HasForeignKey(_ => _.LangId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}