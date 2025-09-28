using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class MdMainModule
    {
        [Key]
        [StringLength(10)]
        public string MainModuleID { get; set; }   // e.g. "MM001"

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        // store child module IDs as plain text (comma separated or single ID)
        [StringLength(200)]
        public string ChildModuleIDs { get; set; }  // Example: "M001,M002,M003"
    }
}
