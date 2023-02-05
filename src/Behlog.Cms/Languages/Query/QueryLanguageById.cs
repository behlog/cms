namespace Behlog.Cms.Query;


public class QueryLanguageById : IBehlogQuery<LanguageResult>
{

    public QueryLanguageById(Guid id)
    {
        id.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        Id = id;
    }
    
    public Guid Id { get; }
}