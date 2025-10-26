using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Entities
{
    public class JobType
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
