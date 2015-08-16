using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSurvey.Data.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string SurveyRespondentId { get; set; }

        public string AnswerValue { get; set; }
    }
}
