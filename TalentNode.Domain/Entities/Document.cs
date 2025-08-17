using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class Document
    {
        public int DocumentID { get; set; }      // Primary Key
        public int EmployeeID { get; set; }      // Foreign Key (link to Employee table)

        public string DocName { get; set; }      // e.g. "Resume", "ID Proof"
        public string FileName { get; set; }     // Actual saved file name
        public string FilePath { get; set; }     // Relative path (e.g. "/Resumes/file.pdf")
        public string FileType { get; set; }     // pdf, docx, jpg, etc.
        public string Link { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        // Navigation Property (optional, if you have Employee model)
       
    }
}
