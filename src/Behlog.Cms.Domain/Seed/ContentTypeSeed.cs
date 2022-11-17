using Behlog.Cms.Commands;
using Behlog.Cms.Resources;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.Seed;

internal class ContentTypeSeed
{
    private readonly IContentTypeWriteStore _writeStore;
    private readonly IBehlogManager _manager;
    
    public ContentTypeSeed(
        IContentTypeWriteStore writeStore, IBehlogManager manager)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
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