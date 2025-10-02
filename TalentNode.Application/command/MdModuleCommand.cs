//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TalentNode.Application.command
//{
//    internal class MdModuleCommand
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;

namespace TalentNode.Application.command
{
    public record MdModuleCommand(int roleid) : IRequest<List<ModulesByRole>>;

    public class MdModuleCommandHandler(IMdModuleRepository IMdModuleRepository) : IRequestHandler<MdModuleCommand, List<ModulesByRole>>
    {
        public async Task<List<ModulesByRole>> Handle(MdModuleCommand request, CancellationToken cancellationToken)
        {
            return await IMdModuleRepository.GetModules(request.roleid);
        }
    }
}
