using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
    }
}
