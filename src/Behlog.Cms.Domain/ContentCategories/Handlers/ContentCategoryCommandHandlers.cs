using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Repository;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;

public class ContentCategoryCommandHandlers :
    IBehlogCommandHandler<CreateContentCategoryCommand, ContentCategoryResult>,
    IBehlogCommandHandler<UpdateContentCategoryCommand>,
    IBehlogCommandHandler<SoftDeleteContentCategoryCommand>
{
    private readonly IBehlogManager _manager;
    private readonly IContentCategoryRepository _contentCategoryRepository;


    public ContentCategoryCommandHandlers(
        IBehlogManager manager, IContentCategoryRepository contentCategoryRepository)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _contentCategoryRepository = contentCategoryRepository 
            ?? throw new ArgumentNullException(nameof(contentCategoryRepository));
    }
    
    public async Task<ContentCategoryResult> HandleAsync(
        CreateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await ContentCategory.CreateAsync(command, _manager);
        await _contentCategoryRepository.AddAsync(category, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(category.ToResult());
    }

    public Task HandleAsync(UpdateContentCategoryCommand message, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(SoftDeleteContentCategoryCommand message,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}