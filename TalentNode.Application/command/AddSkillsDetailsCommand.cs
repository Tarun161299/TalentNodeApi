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
    public record AddSkillsDetailsCommand(List<SkillAdd> Employee) : IRequest<int>;

    public class AddSkillsDetailsCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<AddSkillsDetailsCommand, int>
    {
        public async Task<int> Handle(AddSkillsDetailsCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddskillsAsync(request.Employee);
        }
    }
}
