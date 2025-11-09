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


    public record ApplyJobCommand(ApplyForJob AppJob) : IRequest<int>;

    public class ApplyJobCommandHandler(IJobRepository IJobRepository) : IRequestHandler<ApplyJobCommand, int>
    {
        public async Task<int> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
        {
            return await IJobRepository.ApplyForJob(request.AppJob);
        }
    }
}
