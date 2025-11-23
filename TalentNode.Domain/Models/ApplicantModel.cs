using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class ApplicantModel
    {
        public int JobId { get; set; }

        public int PageNumber { get; set; } = 1;  // default value = 1

        public int PageSize { get; set; } = 10;   // default value = 10
    }
}
