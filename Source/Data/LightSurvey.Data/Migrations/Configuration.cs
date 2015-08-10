namespace LightSurvey.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LightSurvey.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            ContextKey = "LightSurvey.Data.ApplicationDbContext";
            //TODO: remove
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LightSurvey.Data.ApplicationDbContext context)
        {
        }
    }
}
