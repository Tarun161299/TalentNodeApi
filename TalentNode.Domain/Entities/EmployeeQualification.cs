using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeeQualification
    {
        public int EmpID { get; set; }

        public int QualID { get; set; }

        public string Institute { get; set; }

        public string PassingYesr { get; set; }

        public decimal? Percentage_CGPA { get; set; }
    }
}
