using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public  class JobCreateDto
    {
        public int? JobId { get;set; }
        public string Title { get; set; }
        public int Company { get; set; }
        public int Department { get; set; }
        public string Location { get; set; }
        public int Type { get; set; }
        public int Vacancies { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string SalaryCurrency { get; set; }
        public string Description { get; set; }
        public string ExperienceLevel { get; set; }
        public string? Category { get; set; }
        public List<int> Benefits { get; set; }
        public List<int> Skills { get; set; }
        public string? ApplicationLink { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
    }
}
