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

    public record DepartmentMasterCommand() : IRequest<List<DepartmentModel>>;

    public class DepartmentMasterCommandHandler(IMdRepository IMdRepository) : IRequestHandler<DepartmentMasterCommand, List<DepartmentModel>>
    {
        public async Task<List<DepartmentModel>> Handle(DepartmentMasterCommand request, CancellationToken cancellationToken)
        {
            return await IMdRepository.GetDepartmentMaster();
        }
    }
}
