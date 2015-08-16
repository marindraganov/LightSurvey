namespace LightSurvey.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    public class QuestionLinksViewModel
    {
        public string SurveyNumber { get; set; }

        public List<string> QuestionNames { get; set; }

        public string CurrentQuestion { get; set; }
    }
}