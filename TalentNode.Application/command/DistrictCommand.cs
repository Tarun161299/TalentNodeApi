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
    public record DistrictCommand() : IRequest<List<MdDistrict>>;
    public class DistrictCommandCommandHandler(IMdRepository iMdRepository) : IRequestHandler<DistrictCommand, List<MdDistrict>>
    {
        public async Task<List<MdDistrict>> Handle(DistrictCommand request, CancellationToken cancellationToken)
        {
            return await iMdRepository.GetAllDistrict();
        }
    }
}