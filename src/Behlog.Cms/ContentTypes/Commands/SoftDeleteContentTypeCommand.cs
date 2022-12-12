using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Commands
{
    public class SoftDeleteContentTypeCommand : IBehlogCommand
    {
        public SoftDeleteContentTypeCommand(Guid contentTypeId) {
            contentTypeId.ThrowIfGuidIsEmpty(
                new BehlogInvalidEntityIdException(nameof(ContentType)));
            Id = contentTypeId;
        }

        public Guid Id { get; }
    }
}
