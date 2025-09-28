using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class UserDetails
    {

        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string EmailID { get; set; }
        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public DateTime Created_On { get; set; }

        public DateTime Updated_On { get; set; }
    }
}
