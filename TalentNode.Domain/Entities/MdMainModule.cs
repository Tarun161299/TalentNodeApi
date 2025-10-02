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
        [Required]
        [StringLength(10)]
        public string MainModuleID { get; set; }   // e.g. "MM001"

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int Roleid { get; set; }

        // store child module IDs as plain text (comma separated or single ID)
        
    }
}
