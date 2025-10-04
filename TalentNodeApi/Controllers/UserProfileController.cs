using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController(ISender sender) : ControllerBase
    {
        [HttpPost("SaveUserProfile")]
        public async Task<int> UserProfileAsync([FromBody] SignupDetailsModel user)
        {
            try
            {
                int result = await sender.Send(new UserProfileCommand(user));
                return result;
            }
    }
}
