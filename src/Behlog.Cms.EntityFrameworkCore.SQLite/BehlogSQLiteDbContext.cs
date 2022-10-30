using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.SQLite;

public class BehlogSQLiteDbContext : BehlogDbContext
{
    
    public BehlogSQLiteDbContext(DbContextOptions options) : base(options)
    {
    }
}