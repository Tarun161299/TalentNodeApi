using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class RoleMaster
    {
        [Key]
        public int RoleID { get; set; }

        [MaxLength(200)]
        public string RoleDescription { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }
    }
}
