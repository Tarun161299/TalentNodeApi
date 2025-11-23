using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public  class EmailTemplate
    {
       
            public int Id { get; set; }

            public string Name { get; set; }           // e.g. "WelcomeEmail"
            public string Subject { get; set; }        // Email subject line
            public string Body { get; set; }           // HTML or plain text content

            public string Description { get; set; }    // Optional: what this template is for
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        
    }
}
