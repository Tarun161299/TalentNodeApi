using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class SkillMaster
    {
        [Key]
        public int SkillID { get; set; }
         
        [Required, MaxLength(150)]
        public string SkillName { get; set; }

        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
