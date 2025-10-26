using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class BenefitMaster
    {
        [Key]
        public int BenefitId { get; set; }

        [Required, MaxLength(100)]
        public string? BenefitName { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; } = true;
    }
}
