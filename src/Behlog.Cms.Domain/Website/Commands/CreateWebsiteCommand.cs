using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;
    
public class CreateWebsiteCommand : IBehlogCommand<CommandResult<WebsiteResult>>
{
    
    public string Name { get; }
    public string Title { get; }
    public string Description { get; }
    public string Keywords { get; }
    public string Url { get; }
    public string OwnerUserId { get; }
    public Guid? DefaultLangId { get; }
    public string Password { get; }
    public bool IsReadOnly { get; }
    public string Email { get; }
    public string CopyrightText { get; }


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