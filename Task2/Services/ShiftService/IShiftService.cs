using Task2.DTOs;

namespace Task2.Services.ShiftService
{
    public interface IShiftService
    {
        Task<List<DailyShiftDto>> GetShiftScheduleAsync(int employeeId, int month, int year);
        Task<ServiceResult> AssignShiftAsync(ShiftAssignmentRequest request);
    }
}
