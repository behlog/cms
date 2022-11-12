using System.Runtime.CompilerServices;
using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Models;

public class WebsiteResult : BehlogResult
{
    
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string Url { get; set; }
    public string OwnerUserId { get; set; }
    public Guid? DefaultLangId { get; set; }
    public WebsiteStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Password { get; set; }
    public bool IsReadOnly { get; set; }
    public string Email { get; set; }
    public string CopyrightText { get; set; }


    public static WebsiteResult Success()
        => new WebsiteResult();

    public static WebsiteResult WithErrors(IReadOnlyCollection<ValidationError> errors)
    {
        var result = new WebsiteResult();
        result.WithValidationErrors(errors);
        return result;
    }
    
}