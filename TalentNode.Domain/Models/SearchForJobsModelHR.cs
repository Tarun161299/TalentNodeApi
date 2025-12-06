using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class SearchForJobsModelHR
    {
        public int HrId { get; set; }

        public string search { get; set; } = "";

        public string status { get; set; } = "";
    }
}
