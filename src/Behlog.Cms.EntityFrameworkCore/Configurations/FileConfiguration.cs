using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;
using File = Behlog.Cms.Domain.File;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddFileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<File>(file =>
        {
            file.ToTable(FileTableName).HasKey(_ => _.Id);

            file.Property(_ => _.Title).HasMaxLength(1000).IsUnicode();
            file.Property(_ => _.FilePath).HasMaxLength(2000).IsUnicode().IsRequired();
            file.Property(_ => _.AlternateFilePath).HasMaxLength(2000).IsUnicode();
            file.Property(_ => _.AltTitle).HasMaxLength(1000);
            file.Property(_ => _.Extension).HasMaxLength(50).IsUnicode();
            file.Property(_ => _.Url).HasMaxLength(4000).IsUnicode();
            file.Property(_ => _.Status).HasDefaultValue(FileStatus.Created)
                .HasConversion<int>(
                    c => c.Id,
                    c => FileStatus.Find(c));
            file.Property(_ => _.Description).HasMaxLength(2000).IsUnicode();
        });
    }
}