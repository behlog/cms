using System.Runtime.CompilerServices;
using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Models;

public class WebsiteResult : BehlogResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Keywords { get; set; }
    public string? Url { get; set; }
    public string OwnerUserId { get; set; }
    public Guid? DefaultLangId { get; set; }
    public WebsiteStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Password { get; set; }
    public bool IsReadOnly { get; set; }
    public string? Email { get; set; }
    public string? CopyrightText { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime? LastStatusChangedOn { get; set; }
    public string? LastUpdatedByUserId { get; set; }
    public string? LastUpdatedByIp { get; set; }

    public static WebsiteResult Success()
        => new WebsiteResult();

    public WebsiteResult WithValidationErrors(IReadOnlyCollection<ValidationError> errors)
    {
        if (errors is null || !errors.Any())
            return this;
        
        foreach (var error in errors)
        {
            AddValidationError(error);
        }

        return this;
    }
    
}