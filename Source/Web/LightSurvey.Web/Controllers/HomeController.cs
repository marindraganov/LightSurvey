using LightSurvey.Data;
using LightSurvey.Data.Common.Repository;
using LightSurvey.Data.Models;
using LightSurvey.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using LightSurvey.Web.ViewModels.Home;

namespace LightSurvey.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<SliderImage> images;

        public HomeController(IRepository<SliderImage> images)
        {
            this.images = images;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult SliderPartial()
        {
            var images = this.images.All().Project().To<HomeImageViewModel>();

            return PartialView("_ImageSliderPartial", images);
        }
    }
}