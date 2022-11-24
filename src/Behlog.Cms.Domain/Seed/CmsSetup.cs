using Behlog.Cms.Contracts;
using Behlog.Cms.Seed;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Extensions;
using Idyfa.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Setup;

/// <summary>
/// Setup CMS database and seed a website
/// </summary>
public class CmsSetup : ICmsSetup
{
    private readonly IBehlogDbContext _dbContext;
    private readonly IBehlogMediator _mediator;
    private readonly IContentTypeWriteStore _contentTypeWriteStore;
    private readonly ILanguageWriteStore _languageWriteStore;
    private readonly IIdyfaUserManager _userManager;
    private readonly IIdyfaRoleManager _roleManager;
    private readonly ILogger _logger;
    private readonly IWebsiteWriteStore _websiteWriteStore;
    private readonly IWebsiteService _websiteService;

    private readonly ContentTypeSeed _contentTypeSeed;
    private readonly LanguageSeed _languageSeed;
    private readonly UserSeed _userSeed;
    private readonly WebsiteSeed _websiteSeed;
    
    public CmsSetup(
        IBehlogMediator mediator, IBehlogDbContext dbContext,
        IContentTypeWriteStore contentTypeWriteStore,
        ILanguageWriteStore languageWriteStore, ILogger logger,
        IIdyfaUserManager userManager, IIdyfaRoleManager roleManager,
        IWebsiteWriteStore websiteWriteStore, IWebsiteService websiteService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _contentTypeWriteStore = contentTypeWriteStore ?? throw new ArgumentNullException(nameof(contentTypeWriteStore));
        _languageWriteStore = languageWriteStore ?? throw new ArgumentNullException(nameof(languageWriteStore));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _websiteWriteStore = websiteWriteStore ?? throw new ArgumentNullException(nameof(websiteWriteStore));
        _websiteService = websiteService ?? throw new ArgumentNullException(nameof(websiteService));

        _contentTypeSeed = new ContentTypeSeed(_contentTypeWriteStore, _mediator);
        _languageSeed = new LanguageSeed(_languageWriteStore);
        _userSeed = new UserSeed(_userManager, _roleManager);
        _websiteSeed = new WebsiteSeed(_logger, _websiteWriteStore, _websiteService);
    }

    
    public async Task SetupAsync(
        WebsiteSeedData data, CancellationToken cancellationToken = default)
    {
        data.ThrowExceptionIfArgumentIsNull(nameof(data));

        await _dbContext.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
    }
}