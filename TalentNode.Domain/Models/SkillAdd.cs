using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class SkillAdd
    {
        public int skillEmpId {  get; set; }
        public int name { get; set; }
        public string? level { get; set; }
    }
}
