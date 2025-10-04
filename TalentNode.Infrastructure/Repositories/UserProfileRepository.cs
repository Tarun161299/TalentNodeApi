using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Application.command;
using TalentNode.Domain.interfaces;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class UserProfileRepository(TalentNodeDbContext dbContext) : UserProfileInterface

    {
        public async Task<int> Employee(TalentNode.Domain.Models.UserProfileModel userProfile)
        {
            var Employee=dbContext.Employee.Where(x=>x.EmployeeID== userProfile.EmpId).ToList().FirstOrDefault();
            
            //Employee.FirstName = userProfile.FirstName;
            //Employee.LastName = userProfile.LastName;
            //Employee.Email = userProfile.Email;
            //Employee.Phone = userProfile.Phone;
            Employee.WorkingLocation = userProfile.Address;
            Employee.StateID = userProfile.StateId;
            Employee.DistrictID = userProfile.Districtid;
            Employee.CurrentPosition = userProfile.CurrentPosition;
            Employee.CurrentSalary = userProfile.CurrentSallary;
            Employee.ExpectedSalary = userProfile.ExpectedSallary;

            

            dbContext.Employee.Update(Employee);
            
            return dbContext.SaveChanges();
        }
    }
}
