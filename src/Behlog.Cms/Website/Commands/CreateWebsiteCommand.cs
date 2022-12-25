using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;
    
public class CreateWebsiteCommand : IBehlogCommand<CommandResult<WebsiteResult>>
{
    
    public string Name { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Keywords { get; set; }
    public string? Url { get; set; }
    public string OwnerUserId { get; set; }
    public Guid? DefaultLangId { get; set; }
    public string? Password { get; set; }
    public bool IsReadOnly { get; set; }
    public string? Email { get; set; }
    public string? CopyrightText { get; set; }
    
    public CreateWebsiteCommand() { }
    
    public CreateWebsiteCommand(
        string name, string title, string description, string keywords, string url, 
        string ownerUserId, Guid? defaultLangId, string password, bool isReadOnly, 
        string email, string copyrightText)
    {
        Name = name;
        Title = title;
        Description = description;
        Keywords = keywords;
        Url = url;
        OwnerUserId = ownerUserId;
        DefaultLangId = defaultLangId;
        Password = password;
        IsReadOnly = isReadOnly;
        Email = email;
        CopyrightText = copyrightText;
    }
}