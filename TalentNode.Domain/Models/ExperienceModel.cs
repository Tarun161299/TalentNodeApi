using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class ExperienceModel
    {
        public int EmployeeID { get; set; }

        public int ExperienceId { get; set; }
        public string Company { get; set; }

        public string Position { get; set; }
        public bool? current { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
