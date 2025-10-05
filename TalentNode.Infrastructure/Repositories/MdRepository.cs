using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class MdRepository(TalentNodeDbContext dbContext) //: IMdModuleRepository
    {
        public async Task<List<MdState>> GetAllState()
        {
            var state = dbContext.StateMaster;
            var mdsate = from s in state
                         select new MdState
                         {
                             stateId = s.StateID,
                             state = s.StateName
                         };
            return mdsate.ToList();
        }

        public async Task<List<MdDistrict>> GetAllDistrict()
        {
            var District = dbContext.DistrictMaster;
            var mddistrict = from d in District
                         select new MdDistrict
                         {
                             districtId = d.DistrictID,
                             name = d.DistrictName,
                             stateId=d.StateID
                         };
            return mddistrict.ToList();
        }


    }

}
