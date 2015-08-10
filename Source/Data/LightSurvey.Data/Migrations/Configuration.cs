namespace LightSurvey.Data.Migrations
{
    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Data.Models;
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
            //TODO: extract image seeding
            var images = new GenericRepository<SliderImage>(context);
            if (images.All().Count() == 0)
            {
                images.Add(new SliderImage
                {
                    LocalPath = "../../Content/Images/SiteLogo.png",
                    AltName = "Logo"
                });
                images.Add(new SliderImage
                {
                    LocalPath = "../../Content/Slider/Analyze.jpg",
                    AltName = "Analyze"
                });
                images.Add(new SliderImage
                {
                    LocalPath = "../../Content/Slider/Statistics.png",
                    AltName = "Statistics"
                });
            }

            images.SaveChanges();
        }
    }
}
