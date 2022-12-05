using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Configurations;


public static partial class EntityConfigurations
{


    public static void AddWebpageConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Webpage>(page =>
        {
            
        });
    }
    
}