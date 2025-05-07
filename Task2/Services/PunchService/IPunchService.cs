using Task2.DTOs;

namespace Task2.Services.PunchService
{
    public interface IPunchService
    {
        Task<bool> RecordPunchAsync(PunchDto dto);
        Task<bool> CheckLocationWithinFenceAsync(LocationRequest request);
    }
}
