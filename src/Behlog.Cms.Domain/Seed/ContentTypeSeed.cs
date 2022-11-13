using Behlog.Cms.Store;

namespace Behlog.Cms.Seed;

internal class ContentTypeSeed
{
    private readonly IContentTypeWriteStore _writeStore;


    public ContentTypeSeed(IContentTypeWriteStore writeStore)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }
    
    
}