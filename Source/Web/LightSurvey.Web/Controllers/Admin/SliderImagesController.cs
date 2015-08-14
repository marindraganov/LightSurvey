namespace LightSurvey.Web.Controllers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using LightSurvey.Data;
    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Data.Models;
    using LightSurvey.Web.ViewModels.SliderImage;

    using AutoMapper.QueryableExtensions;

    public class SliderImagesController : Controller
    {
        private const string imageSliderDir = "Content/Slider/";
        private IRepository<SliderImage> images;

        public SliderImagesController(IRepository<SliderImage> images)
        {
            this.images = images;
        }

        // GET: SliderImages
        public ActionResult Index()
        {
            var images = this.images.All().Project().To<ListSliderImageViewModel>();

            return View(images);
        }

        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(HttpPostedFileBase imageFile, string AltName)
        {
            if(imageFile != null)
            {
                List<string> fileTypes = new List<string> { "image/jpeg", "image/jpg", "image/bmp", "image/gif", "image/png" };

                if (fileTypes.Contains(imageFile.ContentType.ToLower()))
                {
                    string fileURI = SliderLocalURIFromPath(imageFile.FileName);

                    if(System.IO.File.Exists(fileURI))
                    {
                        TempData["UploadMessage"] = "File with that name already exists";
                        return Redirect("AddImage");
                    }
                    else
                    {
                        imageFile.SaveAs(fileURI);
                        images.Add(new SliderImage() 
                        { 
                            AltName = AltName, 
                            LocalPath = "../../" + imageSliderDir + imageFile.FileName
                        });

                        images.SaveChanges();
                    }

                    TempData["UploadMessage"] = "Image added successfully!";
                }
                else
                {
                    TempData["UploadMessage"] = "This file isn't image!";
                }
            }
            else
            {
                TempData["UploadMessage"] = "No file detected";
            }

            return Redirect("AddImage");
        }

        public ActionResult Delete(int Id)
        {
            var imageForDel = this.images.All().Where(m => m.Id == Id).First();

            if (imageForDel != null)
            {
                string fileURI = SliderLocalURIFromPath(imageForDel.LocalPath);

                if (System.IO.File.Exists(fileURI))
                {
                    System.IO.File.Delete(fileURI);
                }

                this.images.Delete(Id);
                this.images.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var imageForEdit = this.images.All().Where(m => m.Id == Id).Select(m => m);

            if (imageForEdit != null)
            {
                var image = imageForEdit.Project().To<EditSliderImageViewModel>().First();
                return View(image);
            }

            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, String AltName)
        {
            var image = this.images.All().Where(m => m.Id == Id).Select(m => m).First();
            
            if (image != null)
            {
                image.AltName = AltName;
                this.images.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private static string SliderLocalURIFromPath(string path)
        {
            string domainPath = AppDomain.CurrentDomain.BaseDirectory + imageSliderDir;
            string filename = Path.GetFileName(path);
            string fileURI = Path.Combine(domainPath, filename);

            return fileURI;
        }
    }
}