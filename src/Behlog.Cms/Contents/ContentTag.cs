namespace Behlog.Cms.Domain;

public class ContentTag
{
    private ContentTag() { }

    public ContentTag(Guid contentId, Guid tagId)
    {
        ContentId = contentId;
        TagId = tagId;
    }
    
    public Guid ContentId { get; set; }
    public Content Content { get; set; }
    
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}