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

        public ICollection<string> Rows { get; set; }
    }
}