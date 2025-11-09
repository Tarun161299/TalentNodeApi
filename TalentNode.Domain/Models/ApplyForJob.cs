using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class ApplyForJob
    {
     
        public int CandidateId { get; set; }

     
        public int JobId { get; set; }  // Foreign key to Job table

     
        public string Status { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

      
        public string? CreatedBy { get; set; }
    }
}
