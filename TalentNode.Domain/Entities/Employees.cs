using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; } 

        [Required, MaxLength(150)]
        public string Email { get; set; }
       
        public float Experience { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }

        [ForeignKey(nameof(StateMaster))]
        public int StateID { get; set; }
        public StateMaster State { get; set; }

        [ForeignKey(nameof(DistrictMaster))]
        public int DistrictID { get; set; }
        public DistrictMaster District { get; set; }

        public string WorkingLocation {get; set;}
        public string CurrentPosition { get; set; }

        public string ExpectedSalary { get; set; }

        public string CurrentSalary { get; set; }
        public int ResumeID { get; set; }

        public int EmpImageID { get; set; }


        public ICollection<EmployeeQualification> Qualifications { get; set; }
        public ICollection<EmployeeSkill> Skills { get; set; }
    }
}
