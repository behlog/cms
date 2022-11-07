using Behlog.Core;

namespace Behlog.Cms.Events;

public record TagSoftDeletedEvent(
    Guid Id, string Title, string Slug) : IBehlogEvent;