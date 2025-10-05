using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Entities;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController(ISender sender) : ControllerBase
    {
      
        [HttpPost("AddEducation")]
        public async Task<int> AddEducationAsync(List<EducationModel> user)
        {
            try
            {
                int result = await sender.Send(new EducationCommand(user));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
