namespace LightSurvey.Web.ViewModels.SliderImage
{
    using System;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class ListSliderImageViewModel : IMapFrom<SliderImage>
    {
        public int Id { get; set; }

        public string LocalPath { get; set; }

        public string AltName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}