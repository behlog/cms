using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Cms.Exceptions;
using Behlog.Cms.Handlers;
using Behlog.Extensions;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Seed;


internal class WebsiteSeed
{
    private readonly ILogger<WebsiteSeed> _logger;
    private readonly IWebsiteWriteStore _writeStore;
    private readonly IWebsiteService _service;

    public WebsiteSeed(
        ILogger<WebsiteSeed> logger, IWebsiteWriteStore writeStore, IWebsiteService service)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            {
                langId = PersianLanguage.Id;
                _logger.LogInformation($"Using {PersianLanguage.Title} as Language...");
            }
                
            else if (seedData.LangCode.ToLower() == EnglishLanguage.Code.ToLower())
            {
                langId = EnglishLanguage.Id;
                _logger.LogInformation($"Using {EnglishLanguage.Title} as Language...");
            }
            else
            {
                _logger.LogError($"The language '{seedData.LangCode}' not supported.");
                throw new Exception($"Language '{seedData.LangCode}' not supported.");
            }
        }

        var createCommand = new CreateWebsiteCommand(
            seedData.Name, seedData.Title, seedData.Description, seedData.Keywords,
            seedData.Url, ownerUserId, langId, null, false, seedData.Email, seedData.CopyrightText);

        var validation = CreateWebsiteCommandValidator.Run(createCommand);
        if (!validation.IsValid)
        {
            _logger.LogError($"Validation error for Website: {validation.ToString()}");
            throw new BehlogSeedingException();
        }
        
        var website = await Website.CreateAsync(createCommand, _service);
        _writeStore.MarkForAdd(website);
        
        await _writeStore.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        
        _logger.LogInformation($"The Website '{website.Name}' has created successfully.");
        
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