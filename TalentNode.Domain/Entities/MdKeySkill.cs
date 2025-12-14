using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class MdKeySkill
    {
        [Key]
        public int keyskillId { get; set; }

        public string Description { get; set; }

        
    }
}
