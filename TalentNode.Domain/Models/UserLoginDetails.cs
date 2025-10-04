using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class UserLoginDetails
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string EmailID { get; set; }
        public string Password { get; set; }

        public string MobileNumber { get; set; }

        public DateTime Created_On { get; set; }

        public DateTime Updated_On { get; set; }

        public int? roleId { get; set; }

        public int? Emp { get; set; }

        public string? roleName { get; set; }
    }
}
