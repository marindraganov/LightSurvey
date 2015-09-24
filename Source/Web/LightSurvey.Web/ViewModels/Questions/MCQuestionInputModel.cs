namespace LightSurvey.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class MCQuestionInputModel : QuestionModel, IMapFrom<MCQuestion>
    {
        public MCQuestionInputModel()
        {
            this.Rows = new string[3];
        }

        public ICollection<string> Rows { get; set; }
    }
}