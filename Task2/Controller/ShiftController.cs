using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DTOs;
using Task2.Services.ShiftService;

namespace Task2.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet("shift-schedule/{employeeId}")]
        public async Task<IActionResult> GetShiftSchedule(int employeeId, [FromQuery] int month, [FromQuery] int year)
        {
            var result = await _shiftService.GetShiftScheduleAsync(employeeId, month, year);
            return Ok(result);
        }

        [HttpPut("assign")]
        public async Task<IActionResult> AssignShift([FromBody] ShiftAssignmentRequest request)
        {
            var result = await _shiftService.AssignShiftAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}