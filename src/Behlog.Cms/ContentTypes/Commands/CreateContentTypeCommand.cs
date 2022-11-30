using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands
{

    public class CreateContentTypeCommand : IBehlogCommand<CommandResult<ContentTypeResult>>
    {
        
        public CreateContentTypeCommand(
            string systemName,
            string title,
            Guid langId,
            string slug,
            string description = "") {

            if (systemName.IsNullOrEmptySpace())
                throw new BehlogRequiredFieldException(nameof(systemName));

            if (title.IsNullOrEmptySpace())
                throw new BehlogRequiredFieldException(nameof(title));

            SystemName = systemName.Trim()!;
            Title = title?.Trim().CorrectYeKe()!;
            LangId = langId;
            Slug = slug?.Trim()!;
            Description = description?.CorrectYeKe()!;
        }

        public string SystemName { get; }
        public string Title { get; }
        public string Slug { get; }
        public string Description { get; }
        public Guid LangId { get; }
    }
}
