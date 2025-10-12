using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    namespace TalentNode.Domain.Models
    {
        public class EmployeeDocumentModel
        {
           

            public int EmployeeID { get; set; }

            public string DocName { get; set; }

            public string FileName { get; set; }

            public string FileType { get; set; }

            // Base64 string sent from Angular
            public string FileContentBase64 { get; set; }

            public string Mode { get; set; }

            // Optional: can store a file URL or path


        }
    }

}
