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
  

    public record JobTypeCommand() : IRequest<List<JobTypeModel>>;

    public class JobTypeCommanddHandler(IMdRepository IMdRepository) : IRequestHandler<JobTypeCommand, List<JobTypeModel>>
    {
        public async Task<List<JobTypeModel>> Handle(JobTypeCommand request, CancellationToken cancellationToken)
        {
            return await IMdRepository.GetJobType();
        }
    }
}
