using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;

namespace TalentNode.Application.command
{
    public record  AddEmployeeCommand(EmployeEntity Employee):IRequest<EmployeEntity>;

    public class AddEmployeeCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<AddEmployeeCommand, EmployeEntity>
    {
        public async Task<EmployeEntity> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddEmployeeAsync(request.Employee);
        }
    }
}
