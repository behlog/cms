namespace Behlog.Cms.Domain;

public class ContentTag
{
    private ContentTag() { }

    public ContentTag(Guid contentId, Guid tagId)
    {
        ContentId = contentId;
        TagId = tagId;
    }
    
    public Guid ContentId { get; }
    public Content Content { get; }
    
    public Guid TagId { get; }
    public Tag Tag { get; }
}