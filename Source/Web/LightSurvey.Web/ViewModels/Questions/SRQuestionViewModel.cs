namespace LightSurvey.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class SRQuestionViewModel : IMapFrom<SRQuestion>
    {
        [Required]
        public string SurveyNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public ICollection<string> Rows { get; set; }
    }
}