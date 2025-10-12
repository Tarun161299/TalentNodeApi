using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;

namespace TalentNode.Application.command
{
    public record EmployeeSkillCommand(EmployeeSkillModel SaveCount) : IRequest<int>;
    public class EmployeeSkillCommandHandler(IEmployeeSkillRepository employeeSkillRepository) : IRequestHandler<EmployeeSkillCommand, int>
    {


        public async Task<int> Handle(EmployeeSkillCommand request, CancellationToken cancellationToken)
        {
            return await employeeSkillRepository.SaveSkill(request.SaveCount);
        }
    }
}
