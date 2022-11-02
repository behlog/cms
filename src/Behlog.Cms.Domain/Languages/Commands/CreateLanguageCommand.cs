using Behlog.Cms.Domain.Languages.Models;
using Behlog.Core;

namespace Behlog.Cms.Domain.Languages.Commands;

public class CreateLanguageCommand : IBehlogCommand<LanguageResult>
{
    
    public string Name { get; }
    public string Title { get; }
    public string Code { get; }
}