namespace Behlog.Cms.Models;

public class LanguageResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? IsoCode { get; set; }
    public EntityStatus Status { get; set; }
}