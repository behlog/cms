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

    public async Task HandleAsync(
        UpdateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _contentCategoryRepository.FindAsync(command.Id).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        await category.UpdateAsync(command, _manager);
        
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
        await category.SoftDeleteAsync(_manager);
        _contentCategoryRepository.MarkForUpdate(category);
        await _contentCategoryRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}