using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class DocumentDetails
    {
        
        public int DocumentID { get; set; }      // Primary Key

        
        public int EmployeeID { get; set; }      // Foreign Key (link to Employee table)

        
        public string DocName { get; set; }      // e.g. "Resume", "ID Proof"

        
        public string FileName { get; set; }     // Original file name

        
        public string FileType { get; set; }     // pdf, docx, jpg, etc.

       
        public string FileContentBase64 { get; set; }  // Store file as Base64 string

        
        public string Link { get; set; }         // Optional link

        public DateTime UploadDate { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;

     
        public string CreatedBy { get; set; }

 
        public string UpdatedBy { get; set; }

        // Optional navigation property
        // public Employee Employee { get; set; }
    }
}
