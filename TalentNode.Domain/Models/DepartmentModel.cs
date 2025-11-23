using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class DepartmentModel
    {
       
        public int DepartmentId { get; set; }

    
        public string? DepartmentName { get; set; }


        public string? Description { get; set; }

    
        public bool? IsActive { get; set; } = true;

     
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

      
        public string? ModifiedBy { get; set; }
    }
}
