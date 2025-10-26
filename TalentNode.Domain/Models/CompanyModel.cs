using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public  class CompanyModel
    {
        public int CompanyId { get; set; }

     
        public string? CompanyName { get; set; }

        
        public string? Address { get; set; }

     
        public string? City { get; set; }

       
        public string? State { get; set; }

    
        public string? ZipCode { get; set; }

       
        public string? ContactNumber { get; set; }
    }
}
