using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.Exceptions;
using Behlog.Cms.Handlers;
using Behlog.Extensions;

namespace Behlog.Cms.Seed;


internal class WebsiteSeed
{
    private readonly IWebsiteWriteStore _writeStore;
    private readonly IWebsiteService _service;

    public WebsiteSeed(
        IWebsiteWriteStore writeStore, IWebsiteService service)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    
    public async Task<Website> SeedWebsiteAsync(WebsiteSeedData seedData, string ownerUserId)
    {
        seedData.ThrowExceptionIfArgumentIsNull(nameof(seedData));
        if (string.IsNullOrWhiteSpace(ownerUserId))
            throw new ArgumentNullException(nameof(ownerUserId));

        Guid? langId = null;
        if (seedData.LangCode.IsNotNullOrEmpty())
        {
            if (seedData.LangCode.ToLower() == PersianLanguage.Code.ToLower())
                langId = PersianLanguage.Id;
            else if (seedData.LangCode.ToLower() == EnglishLanguage.Code.ToLower())
                langId = EnglishLanguage.Id;
        }

        var createCommand = new CreateWebsiteCommand(
            seedData.Name, seedData.Title, seedData.Description, seedData.Keywords,
            seedData.Url, ownerUserId, langId, null, false, seedData.Email, seedData.CopyrightText);

        var validation = CreateWebsiteCommandValidator.Run(createCommand);
        if (!validation.IsValid)
        {
            throw new BehlogSeedingException();
        }
        
        var website = await Website.CreateAsync(createCommand, _service);
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