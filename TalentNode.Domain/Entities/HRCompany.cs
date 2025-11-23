using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class HRCompany
    {

        [Key]
        public int HRId { get; set; }

        
        public int CompanyId { get; set; }

        public DateTime? AssignedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
