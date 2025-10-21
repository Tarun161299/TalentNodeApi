using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models.TalentNode.Domain.Models;

namespace TalentNode.Application.command
{
    
    public record EmployeeDocumentsCommand(EmployeeDocumentModel Employee) : IRequest<int>;

    public class EmployeeDocumentsCommandHandler(IEmployeeRepository iemployeeRepository) : IRequestHandler<EmployeeDocumentsCommand, int>
    {
        public async Task<int> Handle(EmployeeDocumentsCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.AddDocumentAsync(request.Employee);
        }
    }
}
