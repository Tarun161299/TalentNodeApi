using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    namespace YourNamespace.Models
    {
        public class SaveEmployee
        {
            public int EmpId { get; set; }

            // Personal Info
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Email { get; set; }
            public string Phone { get; set; }

            public string? NoticePeriod { get; set; }

            public string Location { get; set; }

            public int State { get; set; }
            public int District { get; set; }

            public string CurrentPosition { get; set; }

            public decimal? CurrentSallary { get; set; }
            public decimal? ExpectedSallary { get; set; }


            public string Bio { get; set; }

            // Optional attachments
            public int? ResumeID { get; set; }
            public int? EmpImageID { get; set; }
        }
    }

}
