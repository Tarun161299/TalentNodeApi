using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class JobDetails
    {
        [Key]
        public int JobId { get; set; }

        public string? JobTitle { get; set; }

        public int? CompanyID { get; set; }

        public string? Address { get; set; }

        public int? StateId { get; set; }

        public int? District { get; set; }

        public int? jobTypeId { get; set; }

        public int? NoOfOpenings { get; set; }

        public string? MinimumSalary { get; set; }

        public string? MaximumSalary { get; set; }

        public string? Currency { get; set; }

        public string? ApplicationEmail { get; set; }

        public string? JobDescription { get; set; }

        public string? ExperienceLevel { get; set; }

        public int? DepartmentId { get;set; }

        public DateTime? DeadLine { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updateddate { get; set; }

    }
}
