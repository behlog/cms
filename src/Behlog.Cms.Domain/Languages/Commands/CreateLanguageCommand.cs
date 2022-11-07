using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Commands;

public class CreateLanguageCommand : IBehlogCommand<LanguageResult>
{
    
    public string Name { get; }
    public string Title { get; }
    public string Code { get; }
    public string IsoCode { get; }
}