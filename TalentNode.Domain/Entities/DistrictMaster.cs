using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class DistrictMaster
    {
        [Key]
        public int DistrictID { get; set; }

        [Required, MaxLength(100)]
        public string DistrictName { get; set; }

        [ForeignKey(nameof(StateMaster))]
        public int StateID { get; set; }
        public StateMaster State { get; set; } 
    }
}
