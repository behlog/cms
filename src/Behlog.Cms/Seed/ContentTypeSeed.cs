using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;

namespace Behlog.Cms.Seed;


internal class ContentTypeSeed
{
    private readonly IContentTypeWriteStore _writeStore;
    private readonly ISystemDateTime _dateTime;
    
    public ContentTypeSeed(
        IContentTypeWriteStore writeStore, ISystemDateTime dateTime)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
    }


    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        //for english
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = ContentType.Create(
                new CreateContentTypeCommand(
                    systemName, systemName,
                    EnglishLanguage.Id, systemName.ToLower()), _dateTime);
            _writeStore.MarkForAdd(contentType);
        }
        
        //for persian
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = ContentType.Create(
                new CreateContentTypeCommand(
                    systemName, ContentTypes.PersianNames[systemName],
                    PersianLanguage.Id, systemName.ToLower()), _dateTime);
            _writeStore.MarkForAdd(contentType);
        }
        
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}