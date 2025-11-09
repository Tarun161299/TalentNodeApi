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

        [HttpPost("ApplyForJob")]
        public async Task<int> ApplyForJob(ApplyForJob job)
        {
            var result = await sender.Send(new ApplyJobCommand(job));
            return result;
        }

        [HttpPost("UpdateApllicantJobStatus")]
        public async Task<int> UpdateApllicantJobStatus(ApplyForJob job)
        {
            var result = await sender.Send(new UpdateApplicantStatusCommand(job));
            return result;
        }
        [HttpGet("JobsToEmployee")]
        public async Task<List<JobListForEmployee>> JobsToEmployee(int EmpId)
        {
            var result = await sender.Send(new JobsToEmployeeCommand(EmpId));
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
