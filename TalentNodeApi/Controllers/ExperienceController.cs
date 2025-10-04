using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Entities;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController(ISender sender) : ControllerBase
    {
        [HttpPost("SaveExperience")]
        public async Task<int> AddExperienceAsync([FromBody] ExperienceModel user)
        {
            try
            {
                int result = await sender.Send(new ExperienceCommand(user));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    
}
