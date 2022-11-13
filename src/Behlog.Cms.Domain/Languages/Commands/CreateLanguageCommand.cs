using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Commands;

public class CreateLanguageCommand : IBehlogCommand<LanguageResult>
{

    public CreateLanguageCommand(
        string name, string title, string code, string isoCode)
    {
        Name = name;
        Title = title;
        Code = code;
        IsoCode = isoCode;
    }
    
    public string Name { get; }
    public string Title { get; }
    public string Code { get; }
    public string IsoCode { get; }
}