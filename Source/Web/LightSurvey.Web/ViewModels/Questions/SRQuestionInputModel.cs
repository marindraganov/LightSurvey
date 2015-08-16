using LightSurvey.Data.Models;
using LightSurvey.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LightSurvey.Web.ViewModels.Questions
{
    public class SRQuestionInputModel : IMapFrom<SRQuestion>
    {
        public SRQuestionInputModel()
        {
            this.Rows = new string[3];
        }

        [Required]
        public string SurveyNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public ICollection<string> Rows { get; set; }
    }
}