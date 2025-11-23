using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmailOTP
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool IsVerified { get; set; }
    }
}
