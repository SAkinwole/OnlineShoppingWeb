using Microsoft.AspNetCore.Identity;
using OnlineShoppingWeb.Models;

namespace OnlineShoppingWeb.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            //Roles
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                //Users
                //Admin
                var userMananger = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@onlineshop.com";
                var adminUser = await userMananger.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userMananger.CreateAsync(newAdminUser, "Amin@1234");
                    await userMananger.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //User
                string appUserEmail = "user@onlineshop.com";
                var appUser = await userMananger.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "User",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userMananger.CreateAsync(newAppUser, "User@1234");
                    await userMananger.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }
    }
}
