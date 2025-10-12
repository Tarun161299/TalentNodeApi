using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class EducationModel
    {
        public int degEmpId {  get; set; }
        public int degree { get; set; }
        public string Institution { get; set; }

        public decimal? Percentage { get; set; }
        public int Year { get; set; }

    }
}
