using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteFileUploadCommand : IBehlogCommand
{
    public SoftDeleteFileUploadCommand(Guid fileUploadId)
    {
        Id = fileUploadId;
    }
    
    public Guid Id { get; }
}