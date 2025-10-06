using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models.YourNamespace.Models;

namespace TalentNode.Application.command
{
    public record EmployeeDetailsCommand(int empid) : IRequest<UserProfile>;
    public class EmployeeDetailsCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<EmployeeDetailsCommand, UserProfile>
    {
        public async Task<UserProfile> Handle(EmployeeDetailsCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.GetEmployeeDetails(request.empid);
        }
    }
}
