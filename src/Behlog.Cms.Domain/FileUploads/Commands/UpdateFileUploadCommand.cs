using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateFileUploadCommand : IBehlogCommand
{
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public bool Hidden { get; }
    public string Description { get; }
}