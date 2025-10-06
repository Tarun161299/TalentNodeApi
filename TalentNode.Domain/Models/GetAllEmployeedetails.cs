using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    using System;
    using System.Collections.Generic;

    namespace YourNamespace.Models
    {
        public class UserProfile
        {
            public int Id { get; set; }

            // Basic Info
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Bio { get; set; }
            public string Location { get; set; }

            // Job Info
            public string CurrentPosition { get; set; }
            public string CurrentCompany { get; set; }
            public decimal ExpectedSalary { get; set; }
            public int NoticePeriod { get; set; }

            // Media
            public string Avatar { get; set; }
            public string Resume { get; set; }

            // Collections
            public List<EducationDetail> Education { get; set; } = new();
            public List<ExperienceDetail> Experience { get; set; } = new();
            public List<SkillDetail> Skills { get; set; } = new();

            public List<string> Languages { get; set; } = new();

            public SocialLinks SocialLinks { get; set; } = new();
        }

        public class EducationDetail
        {
            public string Degree { get; set; }
            public string Institution { get; set; }
            public int Year { get; set; }
            public double Percentage { get; set; }
        }

        public class ExperienceDetail
        {
            public string Company { get; set; }
            public string Position { get; set; }
            public string StartDate { get; set; }  // e.g. "2022-01"
            public string EndDate { get; set; }    // can be empty for current job
            public bool Current { get; set; }
            public string Description { get; set; }
        }

        public class SkillDetail
        {
            public string Name { get; set; }
            public string Level { get; set; }
        }

        public class SocialLinks
        {
            public string Linkedin { get; set; }
            public string Github { get; set; }
            public string Portfolio { get; set; }
        }
    }

}
