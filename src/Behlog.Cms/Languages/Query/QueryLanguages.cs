namespace Behlog.Cms.Query;


public class QueryLanguages : IBehlogQuery<IReadOnlyCollection<LanguageResult>>
{
	public QueryLanguages(EntityStatus? status = null) {
		Status = status;
	}

    public EntityStatus? Status { get; }
}