namespace Behlog.Cms.Domain;

public class ContentTypeTag : BehlogEntity<long>
{

    #region props

    public Guid ContentTypeId { get; protected set; }
    public Guid TagId { get; protected set; }
    public Guid LangId { get; protected set; }

    public string TagTitle { get; protected set; }
    public string TagSlug { get; protected set; }
    #endregion

    #region Navigations
    public ContentType ContentType { get; protected set; }
    public Tag Tag { get; protected set; }
    public Language Language { get; protected set; }
    
    #endregion

    public static ContentTypeTag New(Guid contentTypeId, Guid tagId)
    {
        var tag = new ContentTypeTag
        {
            ContentTypeId = contentTypeId,
            TagId = tagId
        };
        return tag;
    }


    public ContentTypeTag WithLangId(Guid langId)
    {
        LangId = langId;
        return this;
    }

    public ContentTypeTag WithTagTitle(string tagTitle)
    {
        TagTitle = tagTitle;
        return this;
    }

    public ContentTypeTag WithTagSlug(string tagSlug)
    {
        TagSlug = tagSlug;
        return this;
    }
}