using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentNode.Application.command;
using TalentNode.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalentNodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController(ISender sender) : ControllerBase
    {
        // GET: api/<EmailVerificationController>
        [HttpPost("send-otp")]
        public async Task<int> SendOtp([FromBody] EmailVerification model) {
            try
            {
                int result = await sender.Send(new EmailVerificationCommand(model));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET api/<EmailVerificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmailVerificationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmailVerificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmailVerificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
