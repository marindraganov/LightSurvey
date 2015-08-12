using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LightSurvey.Web.Controllers.Admin
{
    public class CreateSurveyController : Controller
    {

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

            ViewBag.items = surveyNames;

            return View();
        }
    }
}