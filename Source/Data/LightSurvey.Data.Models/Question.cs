namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LightSurvey.Data.Common.Models;

    public enum QuestionType
    {
        SingleResponse,
        MultipleChoise,
        DropDownList,
        DateTime,
        SingleTextBox
    }

    public class Question : AuditInfo, IDeletableEntity
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public QuestionType Type { get; set; }

        [Required]
        public string Text { get; set; }

        public string Name { get; set; }

        public string SurveyNumber { get; set; }

        public virtual Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }

            set
            {
                this.answers = value;
            }
        }

        public bool AnswerIsRequired { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
