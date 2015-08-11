namespace LightSurvey.Web.ViewModels.SliderImage
{
    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class EditSliderImageViewModel : IMapFrom<SliderImage>
    {
        public int Id { get; set; }

        public string LocalPath { get; set; }

        public string AltName { get; set; }
    }
}