using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string CurrentPosition { get; set; }
        public string CurrentCompany { get; set; }
        public string ExpectedSallary { get; set; }
        public string ProfessionalSummary { get; set; }
        
    }
}
