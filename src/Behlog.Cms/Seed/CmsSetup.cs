using Behlog.Core;
using Behlog.Cms.Seed;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Contracts;
using Behlog.Core.Contracts;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Setup;

/// <summary>
/// Setup CMS database and seed a website
/// </summary>
public class CmsSetup : ICmsSetup
{
    private readonly IBehlogDbContext _dbContext;
    private readonly IIdyfaDbContext _idyfaDbContext; 
    private readonly IBehlogMediator _mediator;
    private readonly IContentTypeWriteStore _contentTypeWriteStore;
    private readonly ILanguageWriteStore _languageWriteStore;
    private readonly IIdyfaUserManager _userManager;
    private readonly IIdyfaRoleManager _roleManager;
    private readonly IWebsiteWriteStore _websiteWriteStore;
    private readonly IWebsiteService _websiteService;
    private readonly IContentTypeService _contentTypeService;

    private readonly ContentTypeSeed _contentTypeSeed;
    private readonly LanguageSeed _languageSeed;
    private readonly UserSeed _userSeed;
    private readonly WebsiteSeed _websiteSeed;
    
    public CmsSetup(
        IBehlogMediator mediator, IBehlogDbContext dbContext, IIdyfaDbContext idyfaDbContext,
        IContentTypeWriteStore contentTypeWriteStore, ILanguageWriteStore languageWriteStore, 
        IIdyfaUserManager userManager, IIdyfaRoleManager roleManager,
        IWebsiteWriteStore websiteWriteStore, IWebsiteService websiteService, 
        ISystemDateTime dateTime, IContentTypeService contentTypeService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _idyfaDbContext = idyfaDbContext ?? throw new ArgumentNullException(nameof(idyfaDbContext));
        _contentTypeWriteStore = contentTypeWriteStore ?? throw new ArgumentNullException(nameof(contentTypeWriteStore));
        _languageWriteStore = languageWriteStore ?? throw new ArgumentNullException(nameof(languageWriteStore));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _websiteWriteStore = websiteWriteStore ?? throw new ArgumentNullException(nameof(websiteWriteStore));
        _websiteService = websiteService ?? throw new ArgumentNullException(nameof(websiteService));
        _contentTypeService = contentTypeService ?? throw new ArgumentNullException(nameof(contentTypeService));

        _contentTypeSeed = new ContentTypeSeed(_contentTypeWriteStore, dateTime, contentTypeService);
        _languageSeed = new LanguageSeed(_languageWriteStore);
        _userSeed = new UserSeed(_userManager, _roleManager);
        _websiteSeed = new WebsiteSeed(_websiteWriteStore, _websiteService);
    }

    
    public async Task SetupAsync(
        WebsiteSeedData data,  bool ensureDbCreated = false, CancellationToken cancellationToken = default)
    {
        data.ThrowExceptionIfArgumentIsNull(nameof(data));

        if (ensureDbCreated)
        {
            try
            {
                await _idyfaDbContext.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error]: Creating IdyfaDb {Environment.NewLine} {ex.GetBaseException().Message}");
            }

            try
            {
                await _idyfaDbContext.MigrateDbAsync(cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error]: Migrating IdyfaDb {Environment.NewLine} {ex.GetBaseException().Message}");
            }
            Console.WriteLine("The Idyfa database created and migrated successfully.");

            try
            {
                await _dbContext.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: Creating BehlogDb {Environment.NewLine} {ex.GetBaseException().Message}");
            }

            try
            {
                await _dbContext.MigrateDbAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: Migrating BehlogDb {Environment.NewLine} {ex.GetBaseException().Message}");
            }
            Console.WriteLine("The Behlog database created and migrated successfully.");
        }

        var user = await _userSeed.SeedAdminUserAsync(cancellationToken);
        Console.WriteLine($"[Setup] - SeedUser completed. ('{user.UserName}' user has been created. ");

        await _languageSeed.SeedAsync(cancellationToken);
        Console.WriteLine($"[Setup] - SeedLanguages completed...");

        await _contentTypeSeed.SeedAsync(cancellationToken);
        Console.WriteLine($"[Setup] - SeedContentTypes completed...");

        var website = await _websiteSeed.SeedWebsiteAsync(data, user.Id);
        Console.WriteLine($"[Setup] SeedWebsite completed. The Website '{data.Name}' has been created...");

        Console.WriteLine($"[Setup] DONE. Now you can enjoy Behlog CMS!");
    }
}