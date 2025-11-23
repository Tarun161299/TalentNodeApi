using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;

namespace TalentNode.Application.command
{

    public record JobsToEmployeeCommand(int EmpId) : IRequest<List<JobListForEmployee>>;

    public class JobsToEmployeeCommandHandler(IJobRepository IJobRepository) : IRequestHandler<JobsToEmployeeCommand, List<JobListForEmployee>>
    {
        public async Task<List<JobListForEmployee>> Handle(JobsToEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await IJobRepository.ViewJobs(request.EmpId);
        }
    }
}
