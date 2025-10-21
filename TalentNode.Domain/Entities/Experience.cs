using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class Experience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExperienceID { get; set; }

        [Required]
        [StringLength(200)]
        public string OrganizationName { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? UpdatedOn { get; set; }

        public string? position { get; set; }

        public string? workDescription { get; set; }
    }
}
