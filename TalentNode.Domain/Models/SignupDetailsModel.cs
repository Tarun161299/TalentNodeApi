using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class SignupDetailsModel
    {
        public string Name { get; set; } 

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        [JsonPropertyName("otp")]
        public string Otp { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
