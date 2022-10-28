using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Events;

public class ContentCategorySoftDeletedEvent : BehlogDomainEvent
{
    public ContentCategorySoftDeletedEvent(Guid contentCategoryId)
    {
        contentCategoryId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(ContentCategory)));
        Id = contentCategoryId;
    }
    
    public Guid Id { get; }
}