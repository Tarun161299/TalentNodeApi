using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models.YourNamespace.Models;

namespace TalentNode.Application.command
{
    public record  AddEmployeeCommand(SaveEmployee Employee):IRequest<int>;

    public class AddEmployeeCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<AddEmployeeCommand, int>
    {
        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddEmployeeAsync(request.Employee);
        }
    }
}
