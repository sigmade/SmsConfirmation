using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmsController : ControllerBase
    {
        private readonly SmsConfirmationService _confirmService;  
        public ConfirmsController(SmsConfirmationService confirmService)
        {
            _confirmService = confirmService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode(SendCodeRequest request)
        {
            var result = await _confirmService.SendCode(request);
            return Ok(result); 
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(VerifyRequest request)
        {
            var result = await _confirmService.VerifyCode(request);
            return Ok(result);
        }
    }
}
