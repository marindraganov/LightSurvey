namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LightSurvey.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Survey : IAuditInfo, IDeletableEntity
    {
        private ICollection<Question> questions;
        private ICollection<Respondent> respondents;

        public Survey()
        {
            this.Id = Guid.NewGuid();
            this.Questions = new HashSet<Question>();
            this.Respondents = new HashSet<Respondent>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50), MinLength(1)]
        public string Title { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        //[StringLength(25)]
        public string SurveyNumber { get; set; }

        public bool IsOpen { get; set; }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
            }
        }

        public virtual ICollection<Respondent> Respondents
        {
            get
            {
                return this.respondents;
            }
            set
            {
                this.respondents = value;
            }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
