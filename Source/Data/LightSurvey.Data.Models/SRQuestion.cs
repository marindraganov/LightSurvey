using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSurvey.Data.Models
{
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
