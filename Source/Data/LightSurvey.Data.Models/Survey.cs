﻿namespace LightSurvey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LightSurvey.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Survey : AuditInfo, IDeletableEntity
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

        [Required]
        public ApplicationUser Owner { get; set; }

        [Required]
        [StringLength(25)]
        [Index]
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
    }
}
