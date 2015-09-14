namespace LightSurvey.Data.Models
{
    using System.Collections.Generic;

    public class MCQuestion : Question
    {
        private ICollection<StringData> rows;

        public MCQuestion()
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
