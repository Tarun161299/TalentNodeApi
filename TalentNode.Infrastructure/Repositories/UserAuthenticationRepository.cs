using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;

//using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using Models = TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;
using Entities = TalentNode.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TalentNode.Infrastructure.Repositories
{
    public class UserAuthenticationRepository(TalentNodeDbContext dbContext) : IUserAuthenticationRepository
    {
        //public async Task<string> AuthenticateUser(Models.UserDetails User)
        //{
        //    var userDetails = (from Usd in dbContext.UserDetails
        //                       join URP in dbContext.UserRoleMapping on Usd.UserID.ToString() equals URP.UserName
        //                       join RM in dbContext.RoleMasters on URP.RoleId equals RM.RoleID
        //                       join emp in dbContext.Employee
        //                 on Usd.UserID equals emp.UserID into empGroup
        //                       from employee in empGroup.DefaultIfEmpty()
        //                       join hr in dbContext.HRdetails
        //               on Usd.UserID equals hr.HRId into hrGroup
        //                       from hrDetails in hrGroup.DefaultIfEmpty()
        //                       where Usd.EmailID.ToLower() == User.UserName.ToLower() && Usd.Password == User.Password
        //                       select new Models.UserLoginDetails
        //                       {
        //                           UserID = Usd.UserID,
        //                           UserName = Usd.UserName,
        //                           EmailID = Usd.EmailID,
        //                           Password = Usd.Password,
        //                           MobileNumber = Usd.MobileNumber,
        //                           Created_On = Usd.Created_On,
        //                           Updated_On = Usd.Updated_On,
        //                           roleId = RM.RoleID,
        //                           roleName = RM.Role,
        //                           IdbyUserRole = RM.RoleID == 4 ? employee.EmployeeID : RM.RoleID == 3 ? hrDetails.HRId : null,
        //                       }).ToList();
        //    if (userDetails.Count() > 0)
        //    {
        //        if (User.UserName.Trim().ToLower() == userDetails[0].EmailID.Trim().ToLower() && User.Password == userDetails[0].Password)
        //        {
        //            var token = this.GenerateJwtToken(userDetails[0].roleId.ToString(), userDetails[0].roleName, userDetails[0].UserName, userDetails[0].EmailID, userDetails[0].IdbyUserRole);
        //            return token;
        //        }
        //        else
        //        {
        //            return "401";
        //        }
        //    }
        //    else
        //    {
        //        return "401";
        //    }


        //}
        public async Task<string> AuthenticateUser(Models.UserDetails user)
        {
            var email = user.UserName.Trim().ToLower();
            var password = user.Password; // Ideally hash this
            var details_to_check = dbContext.UserDetails.Where(x => x.EmailID.Trim().ToLower() == email && x.Password == password).FirstOrDefault();
            if (details_to_check == null)
            {
                return  "401";
            }
            var userDetails = await (
                from Usd in dbContext.UserDetails.AsNoTracking()
                join URP in dbContext.UserRoleMapping on Usd.UserID.ToString() equals URP.UserName
                join RM in dbContext.RoleMasters on URP.RoleId equals RM.RoleID
                join emp in dbContext.Employee on Usd.UserID equals emp.UserID into empGroup
                from employee in empGroup.DefaultIfEmpty()
                join hr in dbContext.HRdetails on Usd.UserID equals hr.HRId into hrGroup
                from hrDetails in hrGroup.DefaultIfEmpty()
                where Usd.EmailID.ToLower() == email && Usd.Password == password
                select new Models.UserLoginDetails
                {
                    UserID = Usd.UserID,
                    UserName = Usd.UserName,
                    EmailID = Usd.EmailID,
                    Password = Usd.Password,
                    MobileNumber = Usd.MobileNumber,
                    Created_On = Usd.Created_On,
                    Updated_On = Usd.Updated_On,
                    roleId = RM.RoleID,
                    roleName = RM.Role,
                    IdbyUserRole = RM.RoleID == 4 ? employee.EmployeeID :
                                   RM.RoleID == 3 ? hrDetails.HRId :
                                   null
                }
            ).FirstOrDefaultAsync();

            if (userDetails == null)
                return "401";

            return GenerateJwtToken(
                userDetails.roleId.ToString(),
                userDetails.roleName,
                userDetails.UserName,
                userDetails.EmailID,
                userDetails.IdbyUserRole
            );
        }


        private string GenerateJwtToken(string Roleid, string RoleName, string Username, string Email, int? Emp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("xxxxxxxsssssssdddddddaaaaaaaaaaaaaa");

            // Build the claim list
            var claims = new List<Claim>
    {
        new Claim("UserName", Username),
        new Claim(ClaimTypes.Role, RoleName),
        new Claim("Role_Id", Roleid),
        new Claim("Email", Email)
    };

            // ✅ Conditionally add EmpId if role is 4
            if (Roleid == "4")
            {
                claims.Add(new Claim("EmpId", Emp.ToString()));
            }
            if (Roleid == "3")
            {
                claims.Add(new Claim("HRId", Emp.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "https://localhost:7054",
                Audience = "https://localhost:7054",
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
