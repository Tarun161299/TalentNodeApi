using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class ModulesByRoleRepository(TalentNodeDbContext dbContext) : IMdModuleRepository
    {
        public async Task<List<ModulesByRole>> GetModules(int roleid)
        {
            List<ModulesByRole> result=new List<ModulesByRole>();
            var result1= dbContext.MdMainModule.Where(x=>x.Roleid==roleid).ToList();
            var MdRoleModule =(from  y in result1
                             select new ModulesByRole
                             {
                                 ModuleID      = y.MainModuleID,
                                 ChildModule   =
                                 (from role in dbContext.MdRoleModule join mdmaster in dbContext.MdModule on role.ModuleID equals mdmaster.ModuleID
                                  where mdmaster.ModuleID!=y.MainModuleID && role.MainModuleID==y.MainModuleID
                                  select new MDModule
                                  {
                                      ModuleID = role.ModuleID,
                                      Description=mdmaster.Description,
                                      URL=mdmaster.URL,
                                      Class=mdmaster.Class

                                  }).ToList(),
                                  
                                 Description   = y.Description,
                                 URL           =y.Url,
                                 Class         =y.Class
                             }).ToList();





            return MdRoleModule;

        }
    }
}
