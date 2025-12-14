using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public  class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string Description { get; set; }

        public string Technologies { get; set; }

        public string? ProjectUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
