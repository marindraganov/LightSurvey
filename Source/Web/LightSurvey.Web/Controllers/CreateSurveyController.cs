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
            //TODO: replace with autorized user and maybe remove this.users
            ApplicationUser user = this.users.All().First();

            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerClass;
            ViewBag.SurveyName = SurveyName;

            return View();
        }

        public ActionResult SRQuestionEditorPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            Thread.Sleep(500);
            return Content("_SRQuestionEditorPartial");
        }

        [ChildActionOnly]
        public ActionResult BuilderPartial()
        {
            ViewBag.QEditorContainerClass = GlobalConstants.QEditorContainerClass;
            return PartialView("_BuilderPartial");
        }
    }
}