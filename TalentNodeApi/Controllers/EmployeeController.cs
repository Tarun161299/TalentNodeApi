using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Entities;
using TalentNode.Domain.Models;
using TalentNode.Domain.Models.YourNamespace.Models;

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

        [HttpGet("Get_All_Employee_Data")]
        public async Task<List<Get_All_Employee_Data>> GetAllEmployeAsync()
        {
            var result = await sender.Send(new Get_All_EmployeeCommand());
            return result;
        }

        [HttpGet("GetResume")]
        public async Task<DocumentDetails> GetResumeById(int EmployeeID)
        {
            var result = await sender.Send(new GetDocumentByEmployeeIDCommand(EmployeeID));
            return result;
        }

        [HttpGet("GetEmployeeDetails")]
        public async Task<UserProfile> GetEmployeeDetails(int EmployeeID)
        {
            var result = await sender.Send(new EmployeeDetailsCommand(EmployeeID));
            return result;
        }
    }
}
