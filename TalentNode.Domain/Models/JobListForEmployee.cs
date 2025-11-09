using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class JobListForEmployee
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Department { get; set; }
        public string? Location { get; set; }
        public string[]? Skills { get; set; }
        public string? Description { get; set; }
        public string? Salary { get; set; }
        public string? Experience { get; set; }
        public string? Type { get; set; }
        public int? ApplicantCount { get; set; }
        public int? NewApplicants { get; set; }
        public int? Interviews { get; set; }
        public DateTime? PostedDate { get; set; }

        public string? employeeStatusForJob { get; set; }
    }
}
