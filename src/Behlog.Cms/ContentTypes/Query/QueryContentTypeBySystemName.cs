using Behlog.Core;

namespace Behlog.Cms.Query;

/// <summary>
/// Query <see cref="ContentType"/> by it's SystemName.
/// </summary>
public class QueryContentTypeBySystemName : IBehlogQuery<ContentTypeResult>
{

    public QueryContentTypeBySystemName(string systemName, Guid langId)
    {
        if (systemName.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(systemName));
        
        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        
        SystemName = systemName;
        LangId = langId;
    }
    
    public string SystemName { get; }
    
    public Guid LangId { get; }
}