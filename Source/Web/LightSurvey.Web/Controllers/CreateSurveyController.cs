namespace LightSurvey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    using Microsoft.AspNet.Identity;

    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Data.Models;
    using LightSurvey.Web.Infrastructure;
    using LikeIt.Web.Controllers;
    using LightSurvey.Web.ViewModels.Questions;

    public class CreateSurveyController : Controller
    {
        private IRepository<User> users;
        private IRepository<Survey> surveys;

        public CreateSurveyController(
            IRepository<User> users,
            IRepository<Survey> surveys)
        {
            this.users = users;
            this.surveys = surveys;
        }

        // GET: CreateSurvey
        public ActionResult Index()
        {
            List<SelectListItem> surveyTitles = new List<SelectListItem>();
            var existingSurveyTitles = this.surveys.All().Select(s => new { number = s.SurveyNumber, title = s.Title});

            if (existingSurveyTitles != null)
            {
                foreach (var item in existingSurveyTitles)
                {
                    surveyTitles.Add(new SelectListItem { Value = item.number, Text = item.title });
                }
            }

            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerId;
            ViewBag.items = surveyTitles;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewSurvey(string SurveyName)
        {
            if (SurveyName == null || SurveyName.Length < 1 || SurveyName.Length > 50)
            {
                return RedirectToAction("Index");
            }

            Survey survey = new Survey
            {
                UserId = this.User.Identity.GetUserId(),
                //TODD: be sure number is unique
                SurveyNumber = RandomGenerator.RandomAlphaNumericSeq(25),
                Title = SurveyName
            };

            this.surveys.Add(survey);
            this.surveys.SaveChanges();
            this.surveys.Detach(survey);
            
            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerId;
            ViewBag.SurveyName = survey.Title;
            ViewBag.SurveyNumber = survey.SurveyNumber;

            return this.View("EditSurvey");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSurvey(string ExistingSurveyNumber)
        {
            if (ExistingSurveyNumber == null)
            {
                return this.RedirectToAction("Index");
            }

            Survey survey = this.surveys.All().Where(s => s.SurveyNumber == ExistingSurveyNumber).First();

            if (survey != null)
            {
                this.ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerId;
                this.ViewBag.SurveyName = survey.Title;
                this.ViewBag.SurveyNumber = survey.SurveyNumber;

                return this.View("EditSurvey");
            }

            return Content("You are trying to edin nonexistent survey!");
        }

        public ActionResult SRQuestionEditorPartial()
        {
            return PartialView("_SRQuestionEditorPartial");
        }

        //TODO: Autorized
        public ActionResult QuestionLinksPartial(string surveyNumber)
        {
            var survey = this.surveys.All().Where(s => s.SurveyNumber == surveyNumber).First();
            if (survey != null)
            {
                var questionNames = survey.Questions.Select(q => q.Name).ToList();

                QuestionLinksViewModel model = new QuestionLinksViewModel
                {
                    SurveyNumber = surveyNumber,
                    QuestionNames = questionNames,
                    CurrentQuestion = string.Empty
                };

                return PartialView("_QuestionLinksPartial", model);
            }

            return this.Content("You are trying to get question list of nonexistent survey!");
        }

        [ChildActionOnly]
        public ActionResult BuilderPartial(string surveyNumber)
        {
            this.ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerId;
            this.ViewBag.SurveyNumber = surveyNumber;
            return this.PartialView("_BuilderPartial");
        }
    }
}