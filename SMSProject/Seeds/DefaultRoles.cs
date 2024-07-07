using Microsoft.AspNetCore.Identity;
using SMSProject.Data.Consts;

namespace SMSProject.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any()) 
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));        
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Managers));        
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Parents));        
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Students));        
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Guest));        
            
            }
        }
    }
}
