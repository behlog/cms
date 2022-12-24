using Behlog.Core;
using Behlog.Cms.Query;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Services;


public class ComponentService : IComponentService
{
    private readonly IBehlogMediator _mediator;


    public ComponentService(IBehlogMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <inheritdoc /> 
    public async Task<bool> ComponentNameExistInWebsiteAsync(
        Guid websiteId, Guid langId, Guid componentId, string componentName)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        componentId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));
        if (componentName.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(componentName));

        var query = new QueryComponentByName(websiteId, langId, componentName);
        var result = await _mediator.PublishAsync(query).ConfigureAwait(false);

        return result != null;
    }
}