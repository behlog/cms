using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Commands
{

    public class UpdateContentTypeCommand : IBehlogCommand
    {

        public UpdateContentTypeCommand(
            Guid id, string systemName, string title,
            string lang, string slug, 
            bool enabled = true, string description = "") {
            
            id.ThrowIfGuidIsEmpty(
                new BehlogInvalidEntityIdException(nameof(ContentType)));
            Id = id;
            SystemName = systemName.Trim()!;
            Title = title?.Trim().CorrectYeKe()!;
            Lang = lang?.Trim()!;
            Slug = slug?.Trim()!;
            Description = description?.CorrectYeKe()!;
            Enabled = enabled;
        }

        public Guid Id { get; }
        public string SystemName { get; }
        public string Title { get; }
        public string Slug { get; }
        public string Description { get; }
        public string Lang { get; }
        public bool Enabled { get; }
    }
}
