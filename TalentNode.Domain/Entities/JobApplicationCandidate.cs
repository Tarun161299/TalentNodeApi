using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using TalentNode.Domain.Entities;

namespace TalentNode.Domain.Entities
{
    public class JobApplicationCandidate
    {
        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int JobId { get; set; }  // Foreign key to Job table

        [MaxLength(50)]
        public string Status { get; set; } = "Applied";

        public DateTime AppliedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        // Optional navigation property (if JobDetails table exists)
        // [ForeignKey(nameof(JobId))]
        // public JobDetails Job { get; set; }
    }
}

