using Task2.DTOs;
using Task2.Model;
using Task2.Services;
using Task2.AppDbContext;
using Microsoft.EntityFrameworkCore;
namespace Task2.Services.ShiftService
{
    public class ShiftService : IShiftService
    {
        private readonly appDbContext _context;

        public ShiftService(appDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailyShiftDto>> GetShiftScheduleAsync(int employeeId, int month, int year)
        {
            var assignments = await _context.EmployeeShifts
                .Include(s => s.Shift)
                .Where(s => s.EmployeeId == employeeId &&
                            s.EffectiveFrom.Month <= month && s.EffectiveTo.Month >= month)
                .ToListAsync();

            var result = new List<DailyShiftDto>();

            var daysInMonth = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);
                var assignedShift = assignments.FirstOrDefault(a => a.EffectiveFrom <= currentDate && a.EffectiveTo >= currentDate);
                if (assignedShift != null)
                {
                    result.Add(new DailyShiftDto
                    {
                        Date = currentDate,
                        ShiftName = assignedShift.Shift.ShiftName,
                        StartTime = assignedShift.Shift.StartTime,
                        EndTime = assignedShift.Shift.EndTime
                    });
                }
            }

            return result;
        }

        public async Task<ServiceResult> AssignShiftAsync(ShiftAssignmentRequest request)
        {
            var assignment = new EmployeeShift
            {
                EmployeeId = request.EmployeeId,
                ShiftId = request.ShiftId,
                EffectiveFrom = request.EffectiveFrom,
                EffectiveTo = request.EffectiveTo
            };

            _context.EmployeeShifts.Add(assignment);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Shift assigned successfully.");
        }
    }
}
