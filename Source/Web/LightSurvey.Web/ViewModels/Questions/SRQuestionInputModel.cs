namespace LightSurvey.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class SRQuestionInputModel : QuestionModel, IMapFrom<SRQuestion>
    {
        public SRQuestionInputModel()
        {
            this.Rows = new string[3];
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public ICollection<string> Rows { get; set; }
    }
}