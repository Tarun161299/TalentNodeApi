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
using Models=TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;
using Entities = TalentNode.Domain.Entities;

namespace TalentNode.Infrastructure.Repositories
{
    public class UserAuthenticationRepository(TalentNodeDbContext dbContext): IUserAuthenticationRepository
    {
        public async Task<string> AuthenticateUser (Models.UserDetails User)
        {
            var userDetails = (from Usd in dbContext.UserDetails
                              join URP in dbContext.UserRoleMapping on Usd.UserID.ToString() equals URP.UserName
                            join RM in dbContext.RoleMasters on URP.RoleId equals RM.RoleID
                              where Usd.EmailID.ToLower() == User.UserName.ToLower() && Usd.Password == User.Password
                              select new Models.UserLoginDetails
                              {
                               UserID = Usd.UserID,
                               UserName =Usd.UserName,
                               EmailID =Usd.EmailID,
                               Password =Usd.Password,
                               MobileNumber =Usd.MobileNumber,
                               Created_On =Usd.Created_On,
                               Updated_On =Usd.Updated_On,
                               roleId =RM.RoleID,
                               roleName =RM.Role
    }).ToList();
            if (userDetails.Count() > 0)
            {
                if (User.UserName.Trim().ToLower() == userDetails[0].EmailID.Trim().ToLower() && User.Password == userDetails[0].Password)
                {
                    var token = this.GenerateJwtToken(userDetails[0].roleId.ToString(), userDetails[0].roleName, userDetails[0].UserName, userDetails[0].EmailID);
                    return token;
                }
                else
                {
                    return "401";
                }
            }
            else
            {
                return "401";
            }


        }

        private string GenerateJwtToken(string Roleid,string RoleName,string Username,string Email)

        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("xxxxxxxsssssssdddddddaaaaaaaaaaaaaa");

            var tokenDescriptor = new SecurityTokenDescriptor

            {

                Subject = new ClaimsIdentity(new[] { new Claim("UserName", Username), new Claim(ClaimTypes.Role, RoleName), new Claim("Role_Id", Roleid), new Claim("Email", Email) }),

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
