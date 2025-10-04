using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeeQualification
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int QualificationID { get; set; }

        public string Institute { get; set; }

        public string PassingYesr { get; set; }
        public QualificationMaster Qualification { get; set; } 
    }
}
