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
  
    public record CreateJobCommand(JobCreateDto JobCreateDto) : IRequest<int>;

    public class CreateJobCommandHandler(IJobRepository iJobRepository) : IRequestHandler<CreateJobCommand, int>
    {
        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            return await iJobRepository.SaveJob(request.JobCreateDto);
        }
    }
}
