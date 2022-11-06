using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Commands;

public class CreateTagCommand : IBehlogCommand<TagResult>
{

    public CreateTagCommand(string title, Guid langId)
    {
        Title = title;
        LangId = langId;
    }
    
    public string Title { get; }
    public Guid LangId { get; }
    
}