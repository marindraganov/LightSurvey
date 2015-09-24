
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightSurvey.Web.ViewModels.Questions
{
    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure.Mapping;

    public class DTQuestionInputModel : QuestionModel, IMapFrom<DTQuestion>
    {
        public DTQuestionInputModel()
        {
            ShowDate = true;
            ShowTime = false;
        }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public bool ShowDate { get; set; }

        public bool ShowTime { get; set; }
    }
}