namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LightSurvey.Data.Common.Models;

    public class Survey : AuditInfo, IDeletableEntity
    {
        public Survey()
        {
            this.ID = Guid.NewGuid();
            this.Questions = new HashSet<Question>();
            this.RespondentIDs = new HashSet<string>();
        }

        public Guid ID { get; set; }

        public ApplicationUser Owner { get; set; }

        public string SurveyNumber { get; set; }

        public bool IsOpen { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<string> RespondentIDs { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
