namespace Behlog.Cms.Commands;

public class ContentTagCommand
{
    public ContentTagCommand(Guid tagId)
    {
        TagId = tagId;
    }

    public ContentTagCommand(Guid tagId, string title)
    {
        TagId = tagId;
        Title = title;
    }

    public ContentTagCommand(string title)
    {
        Title = title;
    }
    
    public Guid? TagId { get; }
    public string? Title { get; }
}