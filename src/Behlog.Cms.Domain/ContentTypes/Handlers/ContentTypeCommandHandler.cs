using Behlog.Cms.Commands;
using Behlog.Cms.Repository;
using Behlog.Core;
using Behlog.Extensions;
using Behlog.Core.Contracts;
using Behlog.Cms.Commands;
using Behlog.Cms.Models;

namespace Behlog.Cms.Domain.Handlers;


public class ContentTypeCommandHandler :
    IBehlogCommandHandler<CreateContentTypeCommand, ContentTypeResult>,
    IBehlogCommandHandler<UpdateContentTypeCommand>,
    IBehlogCommandHandler<SoftDeleteContentTypeCommand>
{
    private readonly IBehlogManager _manager;
    private readonly IContentTypeRepository _contentTypeRepository;

    public ContentTypeCommandHandler(
        IBehlogManager manager, IContentTypeRepository contentTypeRepository)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _contentTypeRepository = contentTypeRepository 
                                 ?? throw new ArgumentNullException(nameof(contentTypeRepository));
    }


    public async Task<ContentTypeResult> HandleAsync(
        CreateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await ContentType.CreateAsync(command, _manager);
        await _contentTypeRepository.AddAsync(contentType, cancellationToken).ConfigureAwait(false);

        var result = new ContentTypeResult
        {
            Id = contentType.Id,
            Lang = contentType.Lang,
            Title = contentType.Title,
            Slug = contentType.Slug,
            Status = contentType.Status,
            CreateDate = contentType.CreateDate,
            ModifyDate = contentType.ModifyDate,
            SystemName = contentType.SystemName,
            LastStatusChangedOn = contentType.LastStatusChangedOn,
            Description = contentType.Description
        };

        return await Task.FromResult(result);
    }

    public async Task HandleAsync(
        UpdateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await _contentTypeRepository.FindAsync(command.Id).ConfigureAwait(false);
        await contentType.UpdateAsync(command, _manager);
        _contentTypeRepository.MarkForUpdate(contentType);
        await _contentTypeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task HandleAsync(SoftDeleteContentTypeCommand message, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}