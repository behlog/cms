namespace Behlog.Cms.Store;


public interface ILanguageReadStore : IBehlogReadStore<Language, Guid>
{


    Task<Language?> GetByCodeAsync(
        string langCode, CancellationToken cancellationToken = default);


    Task<IReadOnlyCollection<Language>> GetListAsync(
        EntityStatus? status = null, CancellationToken cancellationToken = default);

}