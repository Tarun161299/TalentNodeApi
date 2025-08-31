using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class Get_All_Employee_Data
    {
       public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Qualification> Emp_Qualification { get; set; }
        public float Experience { get; set; }
        public List<Skills> Emp_Skills { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }

        public string? EmpImage { get; set; }


    }
}
