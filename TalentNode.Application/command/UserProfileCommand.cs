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
    public record UserProfileCommand(TalentNode.Domain.Models.UserProfileModel userProfile) : IRequest<int>;
    public class UserProfileCommandHandler(UserProfileInterface userProfileInterface) : IRequestHandler<UserProfileCommand, int>
    {
        public async Task<int> Handle(UserProfileCommand request, CancellationToken cancellationToken)
        {
            return await userProfileInterface.Employee(request.userProfile);
        }

    }
}