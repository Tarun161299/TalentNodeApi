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
    
    public record GetAllKeySkillsCommand() : IRequest<List<MdKeySkillModel>>;
    public class GetAllKeySkillsCommandHandler(IMdRepository iemployeeRepository) : IRequestHandler<GetAllKeySkillsCommand, List<MdKeySkillModel>>
    {
        public async Task<List<MdKeySkillModel>> Handle(GetAllKeySkillsCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.GetAllKeySkills();
        }
    }
}
