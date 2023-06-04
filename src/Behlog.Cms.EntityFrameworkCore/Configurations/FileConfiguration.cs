using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    public static void AddFileUploadConfiguration(this ModelBuilder builder)
    {
        builder.Entity<FileUpload>(file =>
        {
            file.ToTable(FileTableName).HasKey(_ => _.Id);
            
            file.Property(_=> _.Id).ValueGeneratedNever();
            file.Property(_ => _.Title).HasMaxLength(1000).IsUnicode().IsRequired(false);
            file.Property(_ => _.FilePath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            file.Property(_ => _.FileUrl).HasMaxLength(4000).IsUnicode().IsRequired(false);
            file.Property(_ => _.AlternateFilePath).HasMaxLength(2000).IsUnicode().IsRequired(false);
            file.Property(_ => _.AltTitle).HasMaxLength(1000).IsRequired(false);
            file.Property(_ => _.AltFileUrl).HasMaxLength(1000).IsRequired(false);
            file.Property(_ => _.Extension).HasMaxLength(50).IsUnicode().IsRequired(false);
            file.Property(_ => _.Url).HasMaxLength(4000).IsUnicode().IsRequired(false);
            file.Property(_ => _.Status).HasDefaultValue(FileUploadStatus.Created);
            file.Property(_ => _.FileType).HasDefaultValue(FileTypeEnum.Common);
            file.Property(_ => _.Description).HasMaxLength(2000).IsUnicode().IsRequired(false);

            file.HasOne(_ => _.Website)
                .WithMany()
                .HasForeignKey(_ => _.WebsiteId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}