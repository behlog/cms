namespace Behlog.Cms.Query;

public class QueryComponentExistsByName : IBehlogQuery<bool>
{
    public QueryComponentExistsByName(Guid websiteId, Guid langId, Guid componentId, string name)
    {
        WebsiteId = websiteId;
        LangId = langId;
        ComponentId = componentId;
        Name = name;
    }
    
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public Guid ComponentId { get; }
    public string Name { get; }
}