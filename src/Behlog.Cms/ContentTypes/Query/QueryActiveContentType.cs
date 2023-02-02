namespace Behlog.Cms.Query;

/// <summary>
/// Query for a <see cref="ContentType"/> which it's status is <see cref="EntityStatus.Enabled"/>
/// </summary>
public class QueryActiveContentType : IBehlogQuery<ContentTypeResult?>
{

	public QueryActiveContentType(Guid langId, string systemName) {
        LangId = langId;
        SystemName = systemName;
	}


	public QueryActiveContentType(string langCode, string systemName) {
		LangCode = langCode;
		SystemName = systemName;
	}

	public string SystemName { get; }

	public Guid LangId { get; }

	public string LangCode { get; }
}
