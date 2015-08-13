namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LightSurvey.Data.Common.Models;

    public enum RespondentStatus
    {
        Incomplete,
        Complete,
        Terminated
    }

    public class Respondent : AuditInfo, IDeletableEntity
    {
        private ICollection<Answer> answers;

        public Respondent()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        public Survey Survey { get; set; }

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

        public RespondentStatus Status { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
