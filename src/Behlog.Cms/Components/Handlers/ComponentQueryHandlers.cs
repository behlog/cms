namespace Behlog.Cms.Handlers;

public class ComponentQueryHandlers 
    : IBehlogQueryHandler<QueryComponentById, ComponentResult>,
        IBehlogQueryHandler<QueryComponentByName, ComponentResult>,
        IBehlogQueryHandler<QueryComponentExistsByName, bool>
{
    private readonly IComponentReadStore _readStore;
    
    public ComponentQueryHandlers(IComponentReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    
    public async Task<ComponentResult> HandleAsync(
        QueryComponentById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var component = await _readStore.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));

        var result = component.ToResult()
            .WithFiles(component.Files)
            .WithLanguage(component.Language)
            .WithMeta(component.Meta);

        return await Task.FromResult(result);
    }

    public async Task<ComponentResult> HandleAsync(
        QueryComponentByName query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var component = await _readStore.FindAsync(query, cancellationToken).ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));
        
        var result = component!.ToResult()
            .WithFiles(component!.Files)
            .WithLanguage(component.Language)
            .WithMeta(component.Meta);

        return await Task.FromResult(result);
    }

    public async Task<bool> HandleAsync(
        QueryComponentExistsByName query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        return await _readStore.ExistByNameAsync(
            query.WebsiteId, query.LangId, query.ComponentId, query.Name, cancellationToken).ConfigureAwait(false);
    }
}