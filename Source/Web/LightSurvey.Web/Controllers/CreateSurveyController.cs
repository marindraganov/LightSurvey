using LightSurvey.Data.Common.Repository;
using LightSurvey.Data.Models;
using LightSurvey.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LightSurvey.Web.Controllers
{
    public class CreateSurveyController : Controller
    {
        private IRepository<ApplicationUser> users;

        public CreateSurveyController(IRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        // GET: CreateSurvey
        public ActionResult Index()
        {
            List<SelectListItem> surveyNames = new List<SelectListItem>
            {
                new SelectListItem{
                    Text = "MySurvey1"
                },
                new SelectListItem{
                    Text = "MySurvey2"
                }
            };

            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerClass;
            ViewBag.items = surveyNames;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewSurvey(string SurveyName)
        {
            if (SurveyName == null || SurveyName.Length < 1 || SurveyName.Length > 50)
            {
                return RedirectToAction("Index");
            }

            //TODO: replace with autorized user and maybe remove this.users
            ApplicationUser user = this.users.All().First();
            Survey survey = new Survey
            {
                Owner = user,
                //TODD: be sure number is unique
                SurveyNumber = RandomGenerator.RandomAlphaNumericSeq(25),
                Title = SurveyName
            };

            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerClass;
            ViewBag.SurveyName = survey.Title;
            ViewBag.SurveyNumber = survey.SurveyNumber;

            return View();
        }

        public ActionResult SRQuestionEditorPartial()
        {
            Thread.Sleep(500);
            return PartialView("_SRQuestionEditorPartial");
        }

        [ChildActionOnly]
        public ActionResult BuilderPartial(string surveyNumber)
        {
            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerClass;
            ViewBag.SurveyNumber = surveyNumber;
            return PartialView("_BuilderPartial");
        }
    }
}