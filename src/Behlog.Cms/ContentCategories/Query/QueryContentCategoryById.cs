using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Query;


public class QueryContentCategoryById : IBehlogQuery<ContentCategoryResult>
{

    public QueryContentCategoryById(Guid contentCatgoeyId)
    {
        contentCatgoeyId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(ContentCategory))
            );
        Id = contentCatgoeyId;
    }
    
    public Guid Id { get; }
}