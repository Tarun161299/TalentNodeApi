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
    public record ExperienceCommand(ExperienceModel Employee) : IRequest<int>;
    public class ExperienceCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<ExperienceCommand, int>
    {
        public async Task<int> Handle(ExperienceCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddExperienceAsync(request.Employee);
        }
    }
}
