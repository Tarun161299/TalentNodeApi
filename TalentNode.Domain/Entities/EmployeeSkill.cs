using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeeSkill
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int SkillID { get; set; }
        public SkillMaster Skill { get; set; } 
    }
}
