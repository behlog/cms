namespace Behlog.Cms.Handlers;


public class LanguageQueryHandlers :
    IBehlogQueryHandler<QueryLanguages, IReadOnlyCollection<LanguageResult>>
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

        return result.Select(_=> _)
    }
}
