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

            public int? stateid { get; set; }
            public int? districtId { get; set; }

            // Job Info
            public string CurrentPosition { get; set; }
            public string CurrentCompany { get; set; }

            public string CurrentSalary { get; set; }
            public decimal ExpectedSalary { get; set; }
            public int NoticePeriod { get; set; }

            // Media
            public string Avatar { get; set; }
            public string Resume { get; set; }

            // Collections
            public List<EducationDetail> Education { get; set; } = new();
            public List<ExperienceDetail> Experience { get; set; } = new();
            public List<SkillDetail> Skills { get; set; } = new();

            public List<SkillKeyDetail> KeySkills { get; set; } = new();

            public List<ProjectDetail> Projects { get; set; } = new();

            public List<string> Languages { get; set; } = new();

            public SocialLinks SocialLinks { get; set; } = new();

        }

        public class ApplicantProfile
        {
            public int Id { get; set; }

            // Basic Info
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Bio { get; set; }
            public string Location { get; set; }

            public int? stateid { get; set; }
            public int? districtId { get; set; }

            // Job Info
            public string CurrentPosition { get; set; }
            public string CurrentCompany { get; set; }

            public string CurrentSalary { get; set; }
            public decimal ExpectedSalary { get; set; }
            public int NoticePeriod { get; set; }

            // Media
            public string Avatar { get; set; }
            public string Resume { get; set; }

            // Collections
            public List<EducationDetail> Education { get; set; } = new();
            public List<ExperienceDetail> Experience { get; set; } = new();
            public List<SkillDetailApplicant> Skills { get; set; } = new();

            public List<string> Languages { get; set; } = new();

            public SocialLinks SocialLinks { get; set; } = new();

            public string? ApplicantStatus { get; set; }

            public int? ApplicantStatusID { get; set; }

            public DateTime? ApplyDate { get; set; }

            public int TotalRecords { get; set; }
            public int TotalPages { get; set; }

        }


        public class ProjectDetail
        {
            public int ProjectEmpId { get; set; }
            public string name { get; set; }
            public string StartDate { get; set; }   // yyyy-MM-dd
            public string EndDate { get; set; }
            public bool ongoing { get; set; }
            public string Description { get; set; }
            public string Technologies { get; set; }
            public string Url { get; set; }
        }
        public class EducationDetail
        {
            
                 public int degEmpId { get; set; }
            public int Degree { get; set; }
            public string Institution { get; set; }
            public int Year { get; set; }
            public double Percentage { get; set; }
        }

        public class ExperienceDetail
        {
            public int EmployeeID { get; set; }
            public int ExperienceId { get; set; }
            public string Company { get; set; }
            public string Position { get; set; }
            public string StartDate { get; set; }  // e.g. "2022-01"
            public string EndDate { get; set; }    // can be empty for current job
            public bool Current { get; set; }
            public string Description { get; set; }
        }

        public class SkillDetail
        {
            public int skillEmpId { get; set; }
            public int Name { get; set; }

            public string? skillname { get; set; }
            public string Level { get; set; }
        }

        public class SkillKeyDetail
        {
            public int EmpId { get; set; }
            public int KeySkillId { get; set; }

            public string? keyName { get; set; }
            public string level { get; set; }
        }
        public class SkillDetailApplicant
        {
            public int skillEmpId { get; set; }
            public string? Name { get; set; }

            public string? skillname { get; set; }
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
