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
    public record EducationCommand(List<EducationModel> Employee) : IRequest<int>;
    public class EducationCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<EducationCommand, int>
    {
        public async Task<int> Handle(EducationCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddEducationAsync(request.Employee);
        }
    }
}
