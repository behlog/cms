using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class LanguageWriteStore : BehlogEntityFrameworkCoreWriteStore<Language, Guid>,
    ILanguageWriteStore
{
    
    public LanguageWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}