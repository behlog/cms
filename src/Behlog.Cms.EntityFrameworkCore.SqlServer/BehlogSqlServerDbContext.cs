using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.SqlServer;

public class BehlogSqlServerDbContext : BehlogDbContext
{
    
    public BehlogSqlServerDbContext(DbContextOptions options) : base(options)
    {
    }
}