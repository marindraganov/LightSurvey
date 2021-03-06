﻿namespace LightSurvey.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using LightSurvey.Data;
    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Data.Models;
    using LightSurvey.Web.ViewModels.SliderImage;

    public class HomeController : Controller
    {
        private IRepository<SliderImage> images;

        public HomeController(IRepository<SliderImage> images)
        {
            this.images = images;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }

        [ChildActionOnly]
        public ActionResult SliderPartial()
        {
            var images = this.images.All().Project().To<SliderImageViewModel>();

            return this.PartialView("_ImageSliderPartial", images);
        }
    }
}