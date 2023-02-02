namespace Behlog.Cms.Query;


public class QueryLanguages : IBehlogQuery<LanguageResult>
{
	public QueryLanguages(EntityStatus? status = null) {
		Status = status;
	}

    public EntityStatus? Status { get; }
}