using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class SearchForJobsModel
    {
        public int EmpId { get; set; }

        public string search { get; set; } = "";
    }
}
