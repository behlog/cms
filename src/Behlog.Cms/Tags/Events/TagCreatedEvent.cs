using Behlog.Core;

namespace Behlog.Cms.Events;

public record TagCreatedEvent(
    Guid Id, string Title, string Slug, Guid LangId) : IBehlogEvent;
