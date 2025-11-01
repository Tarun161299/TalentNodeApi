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
   
    public record JobListHrCommand(int hrid) : IRequest<List<JobListDto>>;

    public class JobListHrCommanddHandler(IJobRepository IJobRepository) : IRequestHandler<JobListHrCommand, List<JobListDto>>
    {
        public async Task<List<JobListDto>> Handle(JobListHrCommand request, CancellationToken cancellationToken)
        {
            return await IJobRepository.GetJobs(request.hrid);
        }
    }
}
