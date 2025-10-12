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
    public class EmployeeSkillRepository(TalentNodeDbContext dbContext) : IEmployeeSkillRepository
    {
        public async Task<int> SaveSkill(TalentNode.Domain.Models.EmployeeSkillModel skill)
        {
            EmployeeSkill employeeSkill = new EmployeeSkill();
            employeeSkill.EmployeeID = skill.EmpID;
            employeeSkill.SkillID = skill.SkillID;
            employeeSkill.level = skill.level;
            dbContext.EmployeeSkill.Add(employeeSkill);
            return dbContext.SaveChanges();
          

        }
    }
}
