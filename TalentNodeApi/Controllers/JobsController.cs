using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController(ISender sender) : ControllerBase
    {
        [HttpPost("SaveJobs")]
        public async Task<int> SaveJobs(JobCreateDto job)
        {
            var result = await sender.Send(new CreateJobCommand(job));
            return result;
        }
    }
}
