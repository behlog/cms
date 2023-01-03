namespace Behlog.Cms.Query;

public class QueryComponentByName : IBehlogQuery<ComponentResult>
{

    public QueryComponentByName(Guid websiteId, Guid langId, string name)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));
        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        
        WebsiteId = websiteId;
        LangId = langId;
        Name = name;
    }

    public QueryComponentByName(Guid websiteId, string langCode, string name)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));

        if (langCode.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(langCode));

        if (name.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(name));
        
        WebsiteId = websiteId;
        LangCode = langCode;
        Name = name;
    }
    
    
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public string Name { get; }
    public string LangCode { get; }
}