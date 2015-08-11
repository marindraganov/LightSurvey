namespace LightSurvey.Web.ViewModels.Home
{
    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class HomeImageViewModel : IMapFrom<SliderImage>
    {
        public string LocalPath { get; set; }

        public string AltName { get; set; }
    }
}