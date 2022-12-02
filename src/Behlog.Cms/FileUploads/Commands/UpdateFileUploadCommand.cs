using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;

public class UpdateFileUploadCommand : IBehlogCommand<CommandResult>
{
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public bool Hidden { get; }
    public string Description { get; }
}