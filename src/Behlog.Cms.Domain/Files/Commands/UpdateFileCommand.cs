using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateFileCommand : IBehlogCommand
{
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public bool Hidden { get; }
    public string Description { get; }
}