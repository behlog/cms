using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Commands
{

    public class CreateContentTypeCommand
    {
        public CreateContentTypeCommand(
            string systemName,
            string title,
            string lang,
            string slug,
            string description = "") {

            if (systemName.IsNullOrEmptySpace())
                throw new BehlogRequiredFieldException(nameof(systemName));

            if (title.IsNullOrEmptySpace())
                throw new BehlogRequiredFieldException(nameof(title));

            SystemName = systemName.Trim()!;
            Title = title?.Trim().CorrectYeKe()!;
            Lang = lang?.Trim()!;
            Slug = slug?.Trim()!;
            Description = description?.CorrectYeKe()!;
        }

        public string SystemName { get; }
        public string Title { get; }
        public string Slug { get; }
        public string Description { get; }
        public string Lang { get; }
    }
}
