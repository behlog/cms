using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Query;


public class QueryContentTypeById : IBehlogQuery<ContentTypeResult>
{

    public QueryContentTypeById(Guid contentTypeId)
    {
        contentTypeId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException());
        Id = contentTypeId;
    }
    
    public Guid Id { get; }
}