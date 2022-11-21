using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Commands;

namespace Behlog.Cms.Seed;


internal class ContentTypeSeed
{
    private readonly IContentTypeWriteStore _writeStore;
    private readonly IBehlogMediator _mediator;
    
    public ContentTypeSeed(
        IContentTypeWriteStore writeStore, IBehlogMediator mediator)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    public async Task SeedAsync()
    {
        //for english
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = ContentType.Create(
                new CreateContentTypeCommand(
                    systemName, systemName,
                    EnglishLanguage.Id, systemName.ToLower()));
            _writeStore.MarkForAdd(contentType);
        }
        
        //for persian
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = ContentType.Create(
                new CreateContentTypeCommand(
                    systemName, ContentTypes.PersianNames[systemName],
                    PersianLanguage.Id, systemName.ToLower()));
            _writeStore.MarkForAdd(contentType);
        }

        await _writeStore.SaveChangesAsync().ConfigureAwait(false);
    }
}