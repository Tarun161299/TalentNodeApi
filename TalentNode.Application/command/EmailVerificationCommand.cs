using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Domain.Models.YourNamespace.Models;

namespace TalentNode.Application.command
{
    
    public record EmailVerificationCommand(EmailVerification everify) : IRequest<int>;
    public class EmailVerificationCommandHandler(IEmailRepository iEmailRepository) : IRequestHandler<EmailVerificationCommand, int>
    {
        public async Task<int> Handle(EmailVerificationCommand request, CancellationToken cancellationToken)
        {
            return await iEmailRepository.SendSignupOtp(request.everify);
        }
    }
}
