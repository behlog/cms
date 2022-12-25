using Behlog.Cms.Domain;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{
    
    public static void AddLanguageConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Language>(lang =>
        {
            lang.ToTable(LanguageTableName).HasKey(_ => _.Id);

            lang.Property(_ => _.Id).ValueGeneratedNever();
            lang.Property(_ => _.Name).HasMaxLength(100).IsUnicode().IsRequired();
            lang.Property(_ => _.Code).HasMaxLength(10).IsUnicode().IsRequired();
            lang.Property(_ => _.IsoCode).HasMaxLength(20).IsUnicode().IsRequired(false);
            lang.Property(_ => _.Status).HasDefaultValue(EntityStatusEnum.Enabled);
            lang.Property(_ => _.Title).HasMaxLength(256).IsUnicode().IsRequired();
        });
    }
}