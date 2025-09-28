using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class MDModule
    {
        [Key]
        [StringLength(10)]   // Example: "M001"
        public string ModuleID { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string URL { get; set; }

        [StringLength(50)]
        public string Class { get; set; }  // e.g., "fa fa-icon"
    }
}
