using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    public class SkillController(ISender sender) : ControllerBase
    {
        [HttpPost("SaveSkill")]
        public async Task<int> SkillAsync([FromBody] EmployeeSkillModel user)
        {
            try
            {
                int result = await sender.Send(new EmployeeSkillCommand(user));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
    
}
