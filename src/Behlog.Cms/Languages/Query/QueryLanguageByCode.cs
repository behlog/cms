namespace Behlog.Cms.Query;


public class QueryLanguageByCode : IBehlogQuery<LanguageResult>
{

    public QueryLanguageByCode(string code)
    {
        Code = code;
    }
    
    public string Code { get; }
}