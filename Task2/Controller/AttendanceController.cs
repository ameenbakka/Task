using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DTOs;
using Task2.Services.PunchService;

namespace Task2.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IPunchService _punchService;

        public AttendanceController(IPunchService punchService)
        {
            _punchService = punchService;
        }

        [HttpPost("punch")]
        public async Task<IActionResult> Punch([FromBody] PunchDto dto)
        {
            var success = await _punchService.RecordPunchAsync(dto);
            return success ? Ok("Punch recorded") : BadRequest("Failed to record punch");
        }

        [HttpPost("location-check")]
        public async Task<IActionResult> LocationCheck([FromBody] LocationRequest request)
        {
            var isValid = await _punchService.CheckLocationWithinFenceAsync(request);
            return Ok(new { isValid });
        }
    }
}
