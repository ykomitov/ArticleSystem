namespace ArticleSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

            DbSeeder.SeedUsers(context);
            DbSeeder.SeedCategories(context);
            DbSeeder.SeedArticles(context);
        }
    }
}
