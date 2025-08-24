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
    public record GetDocumentByEmployeeIDCommand(int EmployeeID) : IRequest<DocumentDetails>;
    public class GetDocumentByEmployeeID(IEmployeeRepository iemployeeRepository) : IRequestHandler<GetDocumentByEmployeeIDCommand, DocumentDetails>
    {
        public async Task<DocumentDetails> Handle(GetDocumentByEmployeeIDCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.GetDocumentByID(request.EmployeeID);
        }
    }
}
