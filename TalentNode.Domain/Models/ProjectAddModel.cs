using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class ProjectAdd
    {
        public int projectEmpId { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string? endDate { get; set; }
        public bool ongoing { get; set; }
        public string description { get; set; }
        public string technologies { get; set; }
        public string url { get; set; }
    }

}
