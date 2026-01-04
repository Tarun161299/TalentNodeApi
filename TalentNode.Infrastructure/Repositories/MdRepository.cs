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

        public async Task<List<MdKeySkillModel>> GetAllKeySkills()
        {
            var skillMaster = dbContext.MdKeySkill;
            var mdSkill = from d in skillMaster
                          select new MdKeySkillModel
                          {
                              keyskillId = d.keyskillId,
                              Description = d.Description

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

        public async Task<List<BenefitsMasterModel>> GetAllBenefits()
        {
            var BenefitMaster = dbContext.BenefitMaster;
            var mdBenefitMaster = from d in BenefitMaster
                                  select new BenefitsMasterModel
                                  {
                                      BenefitId = d.BenefitId,
                                      BenefitName = d.BenefitName

                                  };
            return mdBenefitMaster.ToList();
        }

        public async Task<List<CompanyModel>> GetCompaniesByHRID(int HrId)
        {
            var Company = dbContext.Company;
            var MdCompany = from d in Company
                            join hrc in dbContext.HRCompany on d.CompanyId equals hrc.CompanyId
                            where hrc.HRId==HrId
                                  select new CompanyModel
                                  {
                                      CompanyId = d.CompanyId,
                                      CompanyName = d.CompanyName

                                  };
            return MdCompany.ToList();
        }

        public async Task<List<DepartmentModel>> GetDepartmentMaster()
        {
            var Company = dbContext.DepartmentMaster;
            var DepartmentName = from d in Company
                            
                            select new DepartmentModel
                            {
                                DepartmentId = d.DepartmentId,
                                DepartmentName = d.DepartmentName

                            };
            return DepartmentName.ToList();
        }

        public async Task<List<JobTypeModel>> GetJobType()
        {
            var JobType = dbContext.JobType;
            var MdJobType = from d in JobType

                            select new JobTypeModel
                            {
                                     Id = d.Id,
                                     Description = d.Description,
                                     Name=d.Name

                                 };
            return MdJobType.ToList();
        }
    }

}
