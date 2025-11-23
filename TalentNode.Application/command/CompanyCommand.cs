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
   
    public record CompanyCommand(int hrid) : IRequest<List<CompanyModel>>;

    public class CompanyCommandHandler(IMdRepository IMdRepository) : IRequestHandler<CompanyCommand, List<CompanyModel>>
    {
        public async Task<List<CompanyModel>> Handle(CompanyCommand request, CancellationToken cancellationToken)
        {
            return await IMdRepository.GetCompaniesByHRID(request.hrid);
        }
    }
}
