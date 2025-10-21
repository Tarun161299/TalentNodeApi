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
    

    public record StateCommand() : IRequest<List<MdState>>;
    public class StateCommandHandler(IMdRepository iemployeeRepository) : IRequestHandler<StateCommand, List<MdState>>
    {
        public async Task<List<MdState>> Handle(StateCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.GetAllState();
        }
    }
}
