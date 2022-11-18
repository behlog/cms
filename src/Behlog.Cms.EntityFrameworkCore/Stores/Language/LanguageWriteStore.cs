using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class LanguageWriteStore : BehlogWriteStore<Language, Guid>,
    ILanguageWriteStore
{
    
    public LanguageWriteStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}