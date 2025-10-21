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
   
    public record MDQualCommand() : IRequest<List<MDQualification>>;

    public class MDQualCommandHandler(IMdRepository iMdRepository) : IRequestHandler<MDQualCommand, List<MDQualification>>
    {


        public async Task<List<MDQualification>> Handle(MDQualCommand request, CancellationToken cancellationToken)
        {
            return await iMdRepository.GetAllQualification();
        }
    }
}
