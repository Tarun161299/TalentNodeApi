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
    public record SignupCommand(SignupDetailsModel SaveCount) : IRequest<int>;

    public class SignupCommandHandler(SignupInterface SignupInterface) : IRequestHandler<SignupCommand,int>
    {


        public async Task<int> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            return await SignupInterface.SaveSignup(request.SaveCount);
        }
    }
}
