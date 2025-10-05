using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentNode.Domain.Models
{
    public class MdDistrict
    {
        public int districtId { get; set; }
        public string name { get; set; }
        public int stateId { get; set; }
    }
}
