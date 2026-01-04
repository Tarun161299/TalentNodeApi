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

    public record SaveEmployeeProjectCommand(List<ProjectAdd> Employee) : IRequest<int>;

    public class SaveEmployeeProjectCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<SaveEmployeeProjectCommand, int>
    {
        public async Task<int> Handle(SaveEmployeeProjectCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddProjectsAsync(request.Employee);
        }
    }
}
