using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MdModulesController(ISender sender) : ControllerBase
    {

        [HttpGet("GetModuleByRole")]
        public async Task<List<ModulesByRole>> GetAllModuleAsync(int RoleId)
        {
            var result = await sender.Send(new MdModuleCommand(RoleId));
            return result;
        }
    }
}
