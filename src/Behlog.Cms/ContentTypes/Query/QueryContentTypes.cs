namespace Behlog.Cms.Query;


public class QueryContentTypesByLangId : IBehlogQuery<ContentTypeListResult>
{
    public QueryContentTypesByLangId(Guid langId, EntityStatus? status = null)
    {
        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        LangId = langId;
        Status = status;
    }
    
    public Guid LangId { get; }

    public EntityStatus? Status { get; }
}


public class QueryContentTypesByLangCode : IBehlogQuery<ContentTypeListResult>
{

    public QueryContentTypesByLangCode(string langCode, EntityStatus? status = null)
    {
        if (langCode.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(langCode));

        LangCode = langCode;
        Status = status;
    }
    
    public string LangCode { get; }

    public EntityStatus? Status { get; }
}