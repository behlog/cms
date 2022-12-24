using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;

public class ComponentQueryHandlers 
    : IBehlogQueryHandler<QueryComponentById, ComponentResult>,
        IBehlogQueryHandler<QueryComponentByName, ComponentResult>
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

        var component = await _readStore.GetByNameAsync(
                query.WebsiteId, query.LangId, query.Name, cancellationToken)
            .ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));
        
        var result = component!.ToResult()
            .WithFiles(component!.Files)
            .WithLanguage(component.Language)
            .WithMeta(component.Meta);

        return await Task.FromResult(result);
    }
}