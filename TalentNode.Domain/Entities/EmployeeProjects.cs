using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeeProjects
    {
        public int EmpId { get; set; }

        public int ProjectId { get; set; }

        public bool iscurrentlyworking { get; set; }
    }
}
