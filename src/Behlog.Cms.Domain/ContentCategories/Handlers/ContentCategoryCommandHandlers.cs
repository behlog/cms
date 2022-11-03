using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;
using Behlog.Cms.Repository;

namespace Behlog.Cms.Handlers;

public class ContentCategoryCommandHandlers :
    IBehlogCommandHandler<CreateContentCategoryCommand, ContentCategoryResult>,
    IBehlogCommandHandler<UpdateContentCategoryCommand>,
    IBehlogCommandHandler<SoftDeleteContentCategoryCommand>,
    IBehlogCommandHandler<RemoveContentCategoryCommand>
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

        var category = ContentCategory.Create(command);
        await _contentCategoryRepository.AddAsync(category, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(category.ToResult());
    }

    public async Task HandleAsync(
        UpdateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _contentCategoryRepository.FindAsync(command.Id).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
        category.Update(command);
        _contentCategoryRepository.MarkForUpdate(category);
        await _contentCategoryRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _contentCategoryRepository.FindAsync(command.Id).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
        category.SoftDelete();
        _contentCategoryRepository.MarkForUpdate(category);
        await _contentCategoryRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }


    public async Task HandleAsync(
        RemoveContentCategoryCommand command, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _contentCategoryRepository.FindAsync(command.Id).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));

        await _contentCategoryRepository.DeleteAsync(category, cancellationToken).ConfigureAwait(false);
    }
}