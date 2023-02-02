using Idyfa.Core;

namespace Behlog.Cms.Seed;

internal class UserSeed
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

    
    /// <summary>
    /// This will creates Admin Role, Admin User if not exists, and Add AdminRole to the AdminUser.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="BehlogSeedingException"></exception>
    public async Task<User> SeedAdminUserAsync(CancellationToken cancellationToken = default)
    {
        var adminRole = Role.New()
            .WithName("Admin")
            .WithTitle("Administrator");

        if(!await _roleManager.RoleExistsAsync(adminRole.Name)) {
            var roleResult = await _roleManager.CreateAsync(adminRole).ConfigureAwait(false);
            if (!roleResult.Succeeded) {
                Console.WriteLine($"Error : Adding AdminRole {roleResult.Errors.ToList()}");
                //TODO : made an extension method to convert identity errors to string in Idyfa
                throw new BehlogSeedingException(nameof(Role));
            }
        }
        
        // check if user does not exist then create it.
        
        var adminUser = User.RegisterUser(
            defaultAdminUserName, defaultAdminEmail, "Administrator", "", "admin", "admin");
        if (await _userManager.ExistByUserNameAsync(adminUser.Id, adminUser.UserName))
        {
            Console.WriteLine($"[Warning]: AdminUser already existed in the database.");
        }
        else
        {
            var userResult = await _userManager.CreateAsync(adminUser, defaultAdminPassword).ConfigureAwait(false);
            if (!userResult.Succeeded)
            {
                Console.WriteLine($"[Error]: Creating AdminUser : {userResult.Errors.ToString()}");
                foreach (var err in userResult.Errors)
                {
                    Console.WriteLine($"{err.Code}: {err.Description}");
                }
                throw new BehlogSeedingException(nameof(adminUser));
            } 
        }
        
        // check if User does not has the admin role then add to it.
        if (!await _userManager.IsInRoleAsync(adminUser, adminRole.Name))
        {
            var addToRoleResult = await _userManager
                .AddToRoleAsync(adminUser, adminRole.Name).ConfigureAwait(false);
            if (!addToRoleResult.Succeeded)
            {
                throw new BehlogSeedingException(nameof(addToRoleResult));
            }
        }
        
        Console.WriteLine($"[Seed]: User '{adminUser.UserName}' has successfully created.");
        return adminUser;
    }
    
    
    
}