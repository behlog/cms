using Behlog.Cms.Domain;
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
            content.Property(_ => _.CreatedByUserId).HasMaxLength(100);
            content.Property(_ => _.LastUpdatedByUserId).HasMaxLength(100);

            content.HasOne(_ => _.ContentType)
                .WithMany()
                .HasForeignKey(_ => _.ContentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}