using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Domain.Models.YourNamespace.Models;

namespace TalentNode.Application.command
{


    public record ApplicantListCommand(ApplicantModel AppMod) : IRequest<List<ApplicantProfile>>;

    public class ApplicantListCommandHandler(IEmployeeRepository IEmployeeRepository) : IRequestHandler<ApplicantListCommand, List<ApplicantProfile>>
    {
        public async Task<List<ApplicantProfile>> Handle(ApplicantListCommand request, CancellationToken cancellationToken)
        {
            return await IEmployeeRepository.GetApplicantProfile(request.AppMod);
        }
    }
}
