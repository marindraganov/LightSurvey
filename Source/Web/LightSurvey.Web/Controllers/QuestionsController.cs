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
        public ActionResult SRQuestionAddPartial(string surveyNumber)
        {
            var survey = this.surveys.All().Where(s => s.SurveyNumber == surveyNumber).First();

            if (survey != null)
            {
                int questionsCount = survey.Questions.Count();
                string questionName = string.Format("Q{0}", questionsCount + 1);

                var model = new SRQuestionInputModel();
                model.Name = questionName;
                model.SurveyNumber = surveyNumber;

                return this.PartialView("_SRQuestionAddPartial", model);
            }

            return Content("You are trying to add question to nonexistent survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionAddPartial(SRQuestionInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var survey = this.surveys.All().Where(s => s.SurveyNumber == model.SurveyNumber).First();

                //Add new question
                if(survey != null)
                {
                    SRQuestion question = AutoMapper.Mapper.Map<SRQuestion>(model);
                    survey.Questions.Add(question);
                    this.surveys.SaveChanges();
                }
                else
                {
                    return Content("You are trying to add question to nonexistent survey!");
                }
            }
            else
            {
                return PartialView("_SRQuestionAddPartial", model);
            }

            return Content("<h3>The question was saved. Add next question...</h3>");
        }

        [HttpGet]
        public ActionResult SRQuestionEditPartial(string surveyNumber, string questionName)
        {
            var question = this.questions.All().Where(q => 
                q.SurveyNumber == surveyNumber && 
                q.Name == questionName).First();

            if (question != null)
            {
                var model = AutoMapper.Mapper.Map<SRQuestionEditModel>(question);

                return this.PartialView("_SRQuestionEditPartial", model);
            }

            return Content("You are trying to add question to nonexistent survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionEditPartial(SRQuestionEditModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var question = this.questions.All().Where(q =>
                q.SurveyNumber == model.SurveyNumber &&
                q.Name == model.Name).First() as SRQuestion;

                if (question != null)
                {
                    SRQuestion editedQuestion = AutoMapper.Mapper.Map<SRQuestion>(model);
                    Mapper.Map<SRQuestionEditModel, SRQuestion>(model,question);

                    this.questions.SaveChanges();
                }
                else
                {
                    return Content("You are trying to edit nonexistant!");
                }
            }

            return SRQuestionViewPartial(model.SurveyNumber, model.Name);
        }
    }
}