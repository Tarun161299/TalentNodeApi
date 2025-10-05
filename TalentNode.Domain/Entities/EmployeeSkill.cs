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
        
        public int SkillID { get; set; }
   
        public string? level { get; set; }
    }
}
