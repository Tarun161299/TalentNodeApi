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
    //internal class BenefitsMasterCommand
    //{
    //}

    public record BenefitsMasterCommand() : IRequest<List<BenefitsMasterModel>>;

    public class BenefitsMasterCommandHandler(IMdRepository IMdRepository) : IRequestHandler<BenefitsMasterCommand, List<BenefitsMasterModel>>
    {
        public async Task<List<BenefitsMasterModel>> Handle(BenefitsMasterCommand request, CancellationToken cancellationToken)
        {
            return await IMdRepository.GetAllBenefits();
        }
    }
}
