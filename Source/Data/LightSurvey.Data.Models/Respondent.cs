namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LightSurvey.Data.Common.Models;

    enum RespondentStatus
    {
        Incomplete,
        Complete,
        Terminated
    }

    public class Respondent : AuditInfo, IDeletableEntity
    {
        public Respondent()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public Survey Survey { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
