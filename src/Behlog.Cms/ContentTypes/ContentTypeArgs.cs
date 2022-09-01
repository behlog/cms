namespace Behlog.Cms;

public class CreateContentTypeArg 
{
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
}

public class UpdateContentTypeArg
{
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
}