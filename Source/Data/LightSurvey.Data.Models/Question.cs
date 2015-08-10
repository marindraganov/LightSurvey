using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSurvey.Data.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool AnswerIsRequired { get; set; }
    }
}
