using Behlog.Core;

namespace Behlog.Cms.Commands;

public class RemoveFileUploadCommand : IBehlogCommand
{
    public RemoveFileUploadCommand(Guid fileUploadId)
    {
        Id = fileUploadId;
    }
    
    public Guid Id { get; }
}