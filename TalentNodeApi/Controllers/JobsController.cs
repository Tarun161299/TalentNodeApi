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
        [HttpGet("JobsToEmployee")]
        public async Task<List<JobListDto>> JobsToEmployee()
        {
            var result = await sender.Send(new JobsToEmployeeCommand());
            return result;
        }

        [HttpGet("GetJobsHr")]
        public async Task<List<JobListDto>> GetJobs(int hrid)
        {
            var result = await sender.Send(new JobListHrCommand(hrid));
            return result;
        }

        [HttpGet("GetJobsbyjobid")]
        public async Task<JobCreateDto> GetJobDetails(int jobid)
        {
            var result = await sender.Send(new GetJobDetailsCommand(jobid));
            return result;
        }
    }
}
