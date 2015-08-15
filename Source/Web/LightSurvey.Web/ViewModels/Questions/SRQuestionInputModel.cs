using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LightSurvey.Web.ViewModels.Questions
{
    public class SRQuestionInputModel
    {
        public SRQuestionInputModel()
        {
            this.Answers = new string[3];
        }

        [Required]
        [StringLength(25)]
        public string SurveyNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public string[] Answers { get; set; }
    }
}