using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class SignupRepository (TalentNodeDbContext dbContext):SignupInterface
    {
        public async Task<int> SaveSignup(TalentNode.Domain.Models.SignupDetailsModel signup)
        {

            TalentNode.Domain.Entities.UserDetails userDetails = new TalentNode.Domain.Entities.UserDetails();
            userDetails.UserName = signup.Name;
            userDetails.EmailID = signup.Email;
            userDetails.Password = signup.Password;
            userDetails.MobileNumber = signup.PhoneNumber;
            userDetails.Created_On = DateTime.Now;
            userDetails.Updated_On = DateTime.Now;
            TalentNode.Domain.Entities.Employee employee = new TalentNode.Domain.Entities.Employee();
            employee.Email = signup.Email;
            employee.FirstName = signup.Name.Split(" ").Count()>=1? signup.Name.Split(" ")[0]: signup.Name;
            if(signup.Name.Split(" ").Length > 1)
            {
                string[] nameArr = new string[signup.Name.Split(" ").Length - 1];// signup.Name.Split(" ");
                employee.LastName = string.Join(" ", nameArr);
            }
            else
            {
                employee.LastName = " ";
            }
            employee.Phone= signup.PhoneNumber;
            TalentNode.Domain.Entities.UserRoleMapping userRoleMapping = new TalentNode.Domain.Entities.UserRoleMapping();
            
            dbContext.UserDetails.Add(userDetails);
            dbContext.SaveChanges();
            employee.UserID = userDetails.UserID;
            userRoleMapping.UserName = userDetails.UserID.ToString();
            dbContext.Employee.Add(employee);
            userRoleMapping.RoleId = 4;
            dbContext.UserRoleMapping.Add(userRoleMapping);

            return  dbContext.SaveChanges(); 
        }
    }
}
