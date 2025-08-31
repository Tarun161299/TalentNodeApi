using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }      // Primary Key

        
        [Required, MaxLength(200)]
        public string DocName { get; set; }      // e.g. "Resume", "ID Proof"

        [Required, MaxLength(200)]
        public string FileName { get; set; }     // Original file name

        [Required, MaxLength(50)]
        public string FileType { get; set; }     // pdf, docx, jpg, etc.

        [Column(TypeName = "nvarchar(max)")]    // Use NVARCHAR(MAX) in SQL Server
        public string FileContentBase64 { get; set; }  // Store file as Base64 string

        [MaxLength(500)]
        public string Link { get; set; }         // Optional link

        public DateTime UploadDate { get; set; } = DateTime.Now;
        public bool IsRemoved { get; set; } = false;

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        // Optional navigation property
        // public Employee Employee { get; set; }
    }
}
