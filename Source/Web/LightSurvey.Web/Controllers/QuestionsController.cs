namespace LightSurvey.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using LightSurvey.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        [HttpGet]
        public ActionResult SRQuestionEditorPartial(string surveyNumber)
        {
            var model = new SRQuestionInputModel();
            
            return this.PartialView("_SRQuestionEditorPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionEditorPartial(SRQuestionInputModel model)
        {
            return Content("save q");
        }
    }
}