using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;

namespace TalentNode.Application.command
{
    public record Get_All_EmployeeCommand() : IRequest<List<Get_All_Employee_Data>>;
    public class Get_All_Employee(IEmployeeRepository iemployeeRepository) : IRequestHandler<Get_All_EmployeeCommand, List<Get_All_Employee_Data>>
    {
        public async Task<List<Get_All_Employee_Data>> Handle(Get_All_EmployeeCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.Get_All_Employee_Data();
        }
    }
}
