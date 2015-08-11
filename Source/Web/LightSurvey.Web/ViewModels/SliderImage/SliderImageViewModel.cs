namespace LightSurvey.Web.ViewModels.SliderImage
{
    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class SliderImageViewModel : IMapFrom<SliderImage>
    {
        public string LocalPath { get; set; }

        public string AltName { get; set; }
    }
}