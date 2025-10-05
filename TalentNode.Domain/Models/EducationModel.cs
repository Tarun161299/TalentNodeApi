using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class EducationModel
    {
        public int EmployeeID {  get; set; }
        public int QualificationID { get; set; }
        public string Institution { get; set; }
        public string Year { get; set; }

    }
}
