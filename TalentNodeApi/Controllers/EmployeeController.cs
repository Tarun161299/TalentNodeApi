using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Entities;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(ISender sender) : ControllerBase
    { 
        [HttpPost("")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeEntity employee)
        {
            var result = await sender.Send(new AddEmployeeCommand(employee));
            return Ok();
        }
    }
}
