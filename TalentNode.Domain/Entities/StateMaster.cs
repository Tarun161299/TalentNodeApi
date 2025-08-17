using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class StateMaster
    {
        [Key]
        public int StateID { get; set; }

        [Required, MaxLength(100)]
        public string StateName { get; set; }

        public ICollection<DistrictMaster> Districts { get; set; } 
    }
}
