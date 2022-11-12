using Idyfa.Core;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Seed;

public class UserSeed
{
    private readonly IIdyfaUserManager _userManager;
    private readonly IIdyfaRoleManager _roleManager;

    private const string defaultAdminUserName = "admin";
    private const string defaultAdminPassword = "P@$$vv0rd_14";
    private const string defaultAdminEmail = "admin@behlog.org";

    public UserSeed(IIdyfaUserManager userManager, IIdyfaRoleManager roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    
    public async Task SeedAdminUserAsync()
    {
        var adminUser = User.RegisterUser(
            defaultAdminUserName, defaultAdminPassword, defaultAdminEmail,
            "", "admin", "admin");
        var adminRole = Role.New()
            .WithName("Admin")
            .WithTitle("Administrator");
    } 
    
}