using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Models;

namespace TalentNode.Domain.interfaces
{
    public interface IMdRepository
    {
        public Task<List<Skills>> GetAllSkills();

        public Task<List<MdDistrict>> GetAllDistrict();

        public Task<List<MdState>> GetAllState();

        public Task<List<MDQualification>> GetAllQualification();
    }
}
