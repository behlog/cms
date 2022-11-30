using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Extensions;

namespace Behlog.Cms.Commands
{

    public class UpdateContentTypeCommand : IBehlogCommand<CommandResult>
    {

        public UpdateContentTypeCommand(
            Guid id, string systemName, string title,
            Guid langId, string slug, 
            bool enabled = true, string description = "") {
            
            id.ThrowIfGuidIsEmpty(
                new BehlogInvalidEntityIdException(nameof(ContentType)));
            Id = id;
            SystemName = systemName.Trim()!;
            Title = title?.Trim().CorrectYeKe()!;
            LangId = langId;
            Slug = slug?.Trim()!;
            Description = description?.CorrectYeKe()!;
            Enabled = enabled;
        }

        public Guid Id { get; }
        public string SystemName { get; }
        public string Title { get; }
        public string Slug { get; }
        public string Description { get; }
        public Guid LangId { get; }
        public bool Enabled { get; }
    }
}
