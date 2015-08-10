namespace LightSurvey.Data.Models
{
    using LightSurvey.Data.Common.Models;

    public class SliderImage : AuditInfo
    {
        public int Id { get; set; }

        public string LocalPath { get; set; }
    }
}
