namespace LightSurvey.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;

    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Web.ViewModels.Questions;
    using LightSurvey.Data.Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class QuestionsController : Controller
    {
        private IDeletableEntityRepository<Question> questions;
        private IDeletableEntityRepository<Survey> surveys;

        public QuestionsController(IDeletableEntityRepository<Question> questions, IDeletableEntityRepository<Survey> surveys)
        {
            this.questions = questions;
            this.surveys = surveys;
        }

        [HttpGet]
        public ActionResult SRQuestionViewPartial(string surveyNumber, string questionName)
        {
            var question = this.questions.All().Where(q =>  
            q.Name == questionName && q.SurveyNumber == surveyNumber).First();

            if (question != null)
            {
                var model = AutoMapper.Mapper.Map<SRQuestionViewModel>(question);

                return this.PartialView("_SRQuestionViewPartial", model);
            }

            return Content("You are trying to get nonexistent question!");
        }

        [HttpGet]
        public ActionResult SRQuestionEditorPartial(string surveyNumber)
        {
            var survey = this.surveys.All().Where(s => s.SurveyNumber == surveyNumber).First();

            if (survey != null)
            {
                int questionsCount = survey.Questions.Count();
                string questionName = string.Format("Q{0}", questionsCount + 1);

                var model = new SRQuestionInputModel();
                model.Name = questionName;
                model.SurveyNumber = surveyNumber;

                return this.PartialView("_SRQuestionEditorPartial", model);
            }

            return Content("You try to add question to unexisting survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionEditorPartial(SRQuestionInputModel model)
        {
            if (ModelState.IsValid)
            {
                SRQuestion question = AutoMapper.Mapper.Map<SRQuestion>(model);
                var survey = this.surveys.All().Where(s => s.SurveyNumber == question.SurveyNumber).First();
                survey.Questions.Add(question);
                this.surveys.SaveChanges();
            }

            return Content("<h3>The question was saved. Add next question...</h3>");
        }
    }
}