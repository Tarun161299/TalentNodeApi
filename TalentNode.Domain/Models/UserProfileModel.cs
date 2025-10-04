using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class UserProfileModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public int Districtid { get; set; }
        public string CurrentPosition { get; set; }
        public string CurrentSallary { get; set; }
        public string ExpectedSallary { get; set; }
        public int ResumeID { get; set; }
        public int EmpImageID { get; set; }
    }
}
