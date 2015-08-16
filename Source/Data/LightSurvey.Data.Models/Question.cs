namespace LightSurvey.Data.Models
{
    using LightSurvey.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum QuestionType
    {
        MultipleChoise,
        SingleTextBox
    }

    public class Question : AuditInfo, IDeletableEntity
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool AnswerIsRequired { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
