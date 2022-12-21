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
        
        
    }

    public Task<ComponentResult> HandleAsync(
        QueryComponentByName query, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}