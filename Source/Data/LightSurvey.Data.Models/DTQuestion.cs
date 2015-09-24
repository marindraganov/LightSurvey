using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSurvey.Data.Models
{
    public class DTQuestion : Question
    {
        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public bool ShowDate { get; set; }

        public bool ShowTime { get; set; }
    }
}
