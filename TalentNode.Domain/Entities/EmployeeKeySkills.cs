using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeeKeySkills
    {
        public int EmpId { get; set; }

        public int KeySkillId { get; set; }


        public string? level { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
