namespace LightSurvey.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class MCQuestionEditModel : QuestionModel, IMapFrom<MCQuestion>
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Required]
        public ICollection<string> Rows { get; set; }
    }
}