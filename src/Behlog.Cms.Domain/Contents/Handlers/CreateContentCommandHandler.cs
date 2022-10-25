using Behlog.Cms.Commands;
using Behlog.Cms.Repository;
using Behlog.Core;

namespace Behlog.Cms.Domain.Handlers;

public class CreateContentCommandHandler : IBehlogCommandHandler<CreateContentCommand>
{
    private readonly IContentRepository _repository;
    
    public async Task HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        
    }
}