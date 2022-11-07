using Behlog.Core;

namespace Behlog.Cms.Models;

public class LanguageResult : BehlogResult
{
    
    public string Title { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public EntityStatus Status { get; set; }
}