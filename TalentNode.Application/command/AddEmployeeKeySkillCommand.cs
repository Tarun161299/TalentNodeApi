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
 

    public record AddEmployeeKeySkillCommand(List<AddEmployeeKeyskillmodel> Employee) : IRequest<int>;

    public class AddEmployeeKeySkillCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<AddEmployeeKeySkillCommand, int>
    {
        public async Task<int> Handle(AddEmployeeKeySkillCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddKeyskillsAsync(request.Employee);
        }
    }
}
