using Behlog.Cms.Commands;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Seed;


internal class WebsiteSeed
{
    private readonly IWebsiteWriteStore _writeStore;


    public WebsiteSeed(IWebsiteWriteStore writeStore)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }


    public async Task<Website> SeedWebsiteAsync(WebsiteSeedData seedData, string ownerUserId)
    {
        seedData.ThrowExceptionIfArgumentIsNull(nameof(seedData));
        if (string.IsNullOrWhiteSpace(ownerUserId))
            throw new ArgumentNullException(nameof(ownerUserId));

        Guid? langId = null;
        if (seedData.LangCode.IsNotNullOrEmpty())
        {
            if (seedData.LangCode.ToLower() == FarsiLanguage.Code.ToLower())
                langId = FarsiLanguage.Id;
            else if (seedData.LangCode.ToLower() == EnglishLanguage.Code.ToLower())
                langId = EnglishLanguage.Id;
        }

        var website = Website.Create(new CreateWebsiteCommand(
            seedData.Name, seedData.Title, seedData.Description, seedData.Keywords,
            seedData.Url, ownerUserId, langId, null, false, seedData.Email, seedData.CopyrightText));
        
        _writeStore.MarkForAdd(website);
        await _writeStore.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        
        return website;
    }
}

public class WebsiteSeedData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; }
    public string LangCode { get; set; }
    public string Url { get; set; }
    public string Email { get; set; }
    public string CopyrightText { get; set; }
}