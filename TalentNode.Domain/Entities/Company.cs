using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required, MaxLength(150)]
        public string CompanyName { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [MaxLength(15)]
        public string ContactNumber { get; set; }
    }
}
