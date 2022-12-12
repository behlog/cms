using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Core.Contracts;

namespace Behlog.Cms.Seed;


internal class ContentTypeSeed
{
    private readonly IContentTypeWriteStore _writeStore;
    private readonly ISystemDateTime _dateTime;
    private readonly IContentTypeService _service;
    
    public ContentTypeSeed(
        IContentTypeWriteStore writeStore, ISystemDateTime dateTime, IContentTypeService service)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }


    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        //for english
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = await ContentType.CreateAsync(
                new CreateContentTypeCommand(
                    systemName, systemName,
                    EnglishLanguage.Id, systemName.ToLower()), _dateTime, _service);
            _writeStore.MarkForAdd(contentType);
        }
        
        //for persian
        foreach (var systemName in ContentTypes.All)
        {
            var contentType = await ContentType.CreateAsync(
                new CreateContentTypeCommand(
                    systemName, ContentTypes.PersianNames[systemName],
                    PersianLanguage.Id, systemName.ToLower()), _dateTime, _service);
            _writeStore.MarkForAdd(contentType);
        }
        
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}