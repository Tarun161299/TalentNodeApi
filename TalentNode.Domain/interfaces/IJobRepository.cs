using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Domain.Models;

namespace TalentNode.Domain.interfaces
{
    public interface IJobRepository
    {
        public Task<int> SaveJob(JobCreateDto dto);
        public  Task<List<JobListDto>> GetJobs(SearchForJobsModelHR searchModel);
        public Task<JobCreateDto> GetJobById(int jobId);
        public  Task<List<JobListForEmployee>> ViewJobs(SearchForJobsModel searchModel);

        public Task<int> ApplyForJob(ApplyForJob dto);

        public Task<int> UpdateStatusOfEmployee(ApplyForJob dto);
    }
}
