using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class EmployeeSkillModel
    {
        public int EmpID { get; set; }
        public int SkillID { get; set; }
        public string level { get; set; }
    }
}
