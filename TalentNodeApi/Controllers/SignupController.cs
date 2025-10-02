using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Entities;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    public class SignupController(ISender sender) : ControllerBase
    {
        [HttpPost("SaveSignup")]
        public async Task<IActionResult> SignupAsync([FromBody] SignupDetailsModel user)
        {
            var result = await sender.Send(new SignupCommand(user));
            return Ok();
        }

    }
}
