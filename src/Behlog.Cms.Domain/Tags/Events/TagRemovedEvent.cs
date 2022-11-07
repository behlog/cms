using Behlog.Core;

namespace Behlog.Cms.Events;

public record TagRemovedEvent(Guid Id, string Title) : IBehlogEvent;