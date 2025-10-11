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
    public class MdRepository(TalentNodeDbContext dbContext) : IMdRepository
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
                                 stateId = d.StateID
                             };
            return mddistrict.ToList();
        }

        public async Task<List<Skills>> GetAllSkills()
        {
            var skillMaster = dbContext.SkillMaster;
            var mdSkill = from d in skillMaster
                          select new Skills
                          {
                                 SkillID = d.SkillID,
                                 SkillName = d.SkillName
                                
                             };
            return mdSkill.ToList();
        }
        public async Task<List<MDQualification>> GetAllQualification()
        {
            var QualificationMaster = dbContext.QualificationMaster;
            var mdQualificationMaster = from d in QualificationMaster
                          select new MDQualification
                          {
                              QualID = d.QualificationID,
                              QualificationName = d.QualificationName

                          };
            return mdQualificationMaster.ToList();
        }


    }

}
