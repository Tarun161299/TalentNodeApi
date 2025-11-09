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


    public record UpdateApplicantStatusCommand(ApplyForJob AppJob) : IRequest<int>;

    public class UpdateApplicantStatusCommandHandler(IJobRepository IJobRepository) : IRequestHandler<UpdateApplicantStatusCommand, int>
    {
        public async Task<int> Handle(UpdateApplicantStatusCommand request, CancellationToken cancellationToken)
        {
            return await IJobRepository.UpdateStatusOfEmployee(request.AppJob);
        }
    }
}
