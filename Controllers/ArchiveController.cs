using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OffersManagement.Services;

namespace OffersManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly static string PrivateToken = "k1BCgQqAzP5hvTrUNqZ4iHnMKsnObzcbWxsEg84qG4Q6ZSURCUiyi8kgMTv6x8r24qqd4uY9WFra8HJXfZRIFPW52hWRGoFqgndd";
        private readonly IOfferService offerService;

        public ArchiveController(IOfferService offerService)
        {
            this.offerService = offerService;
        }
        [HttpGet("TestCall")]
        public async Task<IActionResult> TestCall(string token)
        {
            if(token != PrivateToken)
            {
                return Ok("Token not valid");
            }
            return Ok("Call is good");
        }
        [HttpGet("StartArchaving")]
        public async Task<IActionResult> ArchiveTask(string token)
        {
            if (token != PrivateToken)
            {
                return Ok("Token not valid");
            }

            try
            {
                await offerService.StartArchiving();
                return Ok("done");
            }
            catch
            {
                return Ok("error");
            }
            

            
        }
    }
}
