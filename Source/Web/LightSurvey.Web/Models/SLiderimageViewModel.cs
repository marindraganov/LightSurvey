using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightSurvey.Web.Models
{
    public class SliderImageViewModel
    {
        public SliderImageViewModel()
        {
        }

        public SliderImageViewModel(string path, string discription)
        {
            this.Path = path;
            this.Discription = discription;
        }

        public string Path { get; set; }

        public string Discription { get; set; }
    }
}