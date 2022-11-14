using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateWebsiteCommand : IBehlogCommand
{
    public UpdateWebsiteCommand(
        Guid id, string name, string title, string description,
        string keywords, string url, string password, bool isReadOnly,
        string email, string copyrightText, Guid? defaultLangId = null)
    {
        Id = id;
        Name = name;
        Title = title;
        Description = description;
        Keywords = keywords;
        Url = url;
        Password = password;
        IsReadOnly = isReadOnly;
        Email = email;
        CopyrightText = copyrightText;
        DefaultLangId = defaultLangId;
    }
    
    public Guid Id { get; }
    public string Name { get; }
    public string Title { get; }
    public string Description { get; }
    public string Keywords { get; }
    public string Url { get; }
    public string Password { get; }
    public bool IsReadOnly { get; }
    public string Email { get; }
    public string CopyrightText { get; }
    public Guid? DefaultLangId { get; }
    
}