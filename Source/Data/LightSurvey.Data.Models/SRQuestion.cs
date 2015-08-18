namespace LightSurvey.Data.Models
{
    using System.Collections.Generic;

    public class SRQuestion : Question
    {
        private ICollection<StringData> rows;

        public SRQuestion()
            : base()
        {
            this.rows = new HashSet<StringData>();
        }

        public virtual ICollection<StringData> Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                this.rows = value;
            }
        }
    }
}
