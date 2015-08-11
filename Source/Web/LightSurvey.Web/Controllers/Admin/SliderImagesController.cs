using LightSurvey.Data;
using LightSurvey.Data.Common.Repository;
using LightSurvey.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LightSurvey.Web.Controllers.Admin
{
    public class SliderImagesController : Controller
    {
        private IRepository<SliderImage> images;

        public SliderImagesController(IRepository<SliderImage> images)
        {
            this.images = images;
        }

        // GET: SliderImages
        public ActionResult Index()
        {
            var images = this.images.All();

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
                    string imageSliderDir = "Content/Slider/";
                    string path = AppDomain.CurrentDomain.BaseDirectory + imageSliderDir;
                    string filename = Path.GetFileName(imageFile.FileName);
                    string fileURI = Path.Combine(path, filename);

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
                this.images.Delete(Id);
                this.images.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}