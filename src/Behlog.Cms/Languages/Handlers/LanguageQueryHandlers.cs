namespace Behlog.Cms.Handlers;


public class LanguageQueryHandlers :
    IBehlogQueryHandler<QueryLanguages, IReadOnlyCollection<LanguageResult>>,
    IBehlogQueryHandler<QueryLanguageById, LanguageResult>,
    IBehlogQueryHandler<QueryLanguageByCode, LanguageResult>
{
    private readonly ILanguageReadStore _readStore;

    public LanguageQueryHandlers(ILanguageReadStore readStore) {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }

    public async Task<IReadOnlyCollection<LanguageResult>> HandleAsync(
        QueryLanguages query, CancellationToken cancellationToken = default) 
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var result = await _readStore.GetListAsync(
            query.Status, cancellationToken).ConfigureAwait(false);

        return result.Select(_ => _.ToResult()).ToList();
    }

    public async Task<LanguageResult> HandleAsync(
        QueryLanguageById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var lang = await _readStore.FindAsync(query.Id, cancellationToken).ConfigureAwait(false);
        lang.ThrowExceptionIfReferenceIsNull($"Language with id: '{query.Id}' not found.");

        return lang.ToResult();
    }

    public async Task<LanguageResult> HandleAsync(
        QueryLanguageByCode query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var lang = await _readStore.GetByCodeAsync(query.Code, cancellationToken).ConfigureAwait(false);
        lang.ThrowExceptionIfReferenceIsNull($"Language with code: '{query.Code}' not found.");

        return lang.ToResult();
    }
    
    
}
