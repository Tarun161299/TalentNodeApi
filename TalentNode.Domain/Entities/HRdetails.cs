using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class HRdetails
    {
        [Key]
        public int HRId { get; set; }

        [Required, MaxLength(100)]
        public string HRName { get; set; }
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Designation { get; set; }
    }
}
