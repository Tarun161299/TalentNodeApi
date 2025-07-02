using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class EmployeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null;

        public string Email {  get; set; }
        public string Phone { get; set; }   
    }
}
