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
    public record skillCommand() : IRequest<List<Skills>>;
    public class skillCommandHandler(IMdRepository iemployeeRepository) : IRequestHandler<skillCommand, List<Skills>>
    {
        public async Task<List<Skills>> Handle(skillCommand request, CancellationToken cancellationToken)
        {
            return await iemployeeRepository.GetAllSkills();
        }
    }
}
