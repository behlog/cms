using Behlog.Cms.Domain;
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


        });
    }
    
}