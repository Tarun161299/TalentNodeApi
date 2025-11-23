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
   
 
    public record GetJobDetailsCommand(int jobid) : IRequest< JobCreateDto>;

    public class GetJobDetailsCommandHandler(IJobRepository iJobRepository) : IRequestHandler<GetJobDetailsCommand, JobCreateDto >
    {
        public async Task<JobCreateDto > Handle(GetJobDetailsCommand request, CancellationToken cancellationToken)
        {
            return await iJobRepository.GetJobById(request.jobid);
        }
    }
}
