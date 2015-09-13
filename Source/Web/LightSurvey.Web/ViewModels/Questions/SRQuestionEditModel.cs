namespace LightSurvey.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class SRQuestionEditModel : QuestionModel, IMapFrom<SRQuestion>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public ICollection<string> Rows { get; set; }
    }
}