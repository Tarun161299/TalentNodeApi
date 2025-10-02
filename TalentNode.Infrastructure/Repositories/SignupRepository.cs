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

            TalentNode.Domain.Entities.UserRoleMapping userRoleMapping = new TalentNode.Domain.Entities.UserRoleMapping();
            userRoleMapping.UserName = signup.Name;
            userRoleMapping.RoleName = signup.Name;

            return 0;
        }
    }
}
