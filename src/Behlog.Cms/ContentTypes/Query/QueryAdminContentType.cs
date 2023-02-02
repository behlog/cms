namespace Behlog.Cms.Query;

/// <summary>
/// Query <see cref="ContentType"/> for Admin panel with support for pagination and filtering.
/// </summary>
public class QueryAdminContentType : IBehlogQuery<QueryResult<ContentTypeResult>>
{

    public QueryAdminContentType(Guid langId, string? systemName, QueryOptions options) {
        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        LangId = langId;
        SystemName = systemName;
        Options = options;

        if(Options is null) {
            Options = QueryOptions.New()
                .WithPageNumber(1).WithPageSize(10)
                .WillOrderBy("id").WillOrderDesc();
        }
    }


    public QueryAdminContentType(string langCode, string? systemName, QueryOptions options) {
        if(langCode.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(langCode));

        LangCode = langCode;
        SystemName = systemName;
        Options = options;

        if(Options is null) {
            Options = QueryOptions.New()
                .WithPageNumber(1).WithPageSize(10)
                .WillOrderBy("id").WillOrderDesc();
        }
    }

    public Guid? LangId { get; }

    public string? LangCode { get; }

    public string? SystemName { get; }

    public QueryOptions Options { get; }


}
