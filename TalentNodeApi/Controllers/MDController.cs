using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MDController(ISender sender)  : ControllerBase
    {
        // GET: api/<MDController>
        [HttpGet("Skills")]
        public async Task<List<Skills>> GetSkills()
        {
            var result = await sender.Send(new skillCommand());
            return result;
        }

        [HttpGet("District")]
        public async Task<List<MdDistrict>> GetDistrict()
        {
            var result = await sender.Send(new DistrictCommand());
            return result;
        }
        [HttpGet("state")]
        public async Task<List<MdState>> GetState() 
        {
            var result = await sender.Send(new StateCommand());
            return result;
        }

        [HttpGet("Qualification")]
        public async Task<List<MDQualification>> GetQualification()
        {
            var result = await sender.Send(new MDQualCommand());
            return result;
        }

        [HttpGet("Company")]
        public async Task<List<CompanyModel>> Company(int hrid)
        {
            var result = await sender.Send(new CompanyCommand(hrid));
            return result;
        }
        [HttpGet("Benifits")]
        public async Task<List<BenefitsMasterModel>> Benifits()
        {
            var result = await sender.Send(new BenefitsMasterCommand());
            return result;
        }
        [HttpGet("Department")]
        public async Task<List<DepartmentModel>> Department()
        {
            var result = await sender.Send(new DepartmentMasterCommand());
            return result;
        }

        [HttpGet("JobType")]
        public async Task<List<JobTypeModel>> GetJobType()
        {
            var result = await sender.Send(new JobTypeCommand());
            return result;
        }
        // GET api/<MDController>/5



    }
}
