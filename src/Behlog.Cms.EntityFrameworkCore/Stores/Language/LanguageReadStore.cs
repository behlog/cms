using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class LanguageReadStore : BehlogReadStore<Language, Guid>,
    ILanguageReadStore
{
    
    public LanguageReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}