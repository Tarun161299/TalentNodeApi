using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
   
    public class MDModule
    {
        public string ModuleID { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Class { get; set; }  // e.g., "fa fa-icon"
    }
}
