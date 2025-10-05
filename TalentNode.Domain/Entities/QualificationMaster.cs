using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class QualificationMaster
    {
        [Key]
        public int QualificationID { get; set; }

        [Required, MaxLength(150)] 
        public string QualificationName { get; set; }

 
    }
}
