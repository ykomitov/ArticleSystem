namespace ArticleSystem.Data.Migrations
{
    using System.Linq;
    using ArticleSystem.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public static class DbSeeder
    {
        public static void SeedUsers(ApplicationDbContext context)
        {
            const string AdministratorUserName = "admin@admin.com";
            const string PowerUserName = "power@user.com";
            const string NormalUserName = "normal@user.com";

            if (!context.Roles.Any())
            {
                // Create roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var adminRole = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(adminRole);

                var powerUserRole = new IdentityRole { Name = GlobalConstants.PowerUserRoleName };
                roleManager.Create(powerUserRole);

                var normalUserRole = new IdentityRole { Name = GlobalConstants.NormalUserRoleName };
                roleManager.Create(normalUserRole);

                // Create users
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName };
                userManager.Create(admin, AdministratorUserName);

                var powerUser = new ApplicationUser { UserName = PowerUserName, Email = PowerUserName };
                userManager.Create(powerUser, PowerUserName);

                var normalUser = new ApplicationUser { UserName = NormalUserName, Email = NormalUserName };
                userManager.Create(normalUser, NormalUserName);

                // Assign users to roles
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
                userManager.AddToRole(powerUser.Id, GlobalConstants.PowerUserRoleName);
                userManager.AddToRole(normalUser.Id, GlobalConstants.NormalUserRoleName);
            }
        }

        public static void SeedCategories(ApplicationDbContext context)
        {
            string[] categories = new string[]
            {
                    "News",
                    "Tech",
                    "Devices",
                    "Soft",
                    "VLog",
                    "About"
            };

            if (!context.ArticleCategories.Any())
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    var newCategory = new ArticleCategory()
                    {
                        Name = categories[i]
                    };

                    context.ArticleCategories.Add(newCategory);
                }
            }
        }
    }
}
