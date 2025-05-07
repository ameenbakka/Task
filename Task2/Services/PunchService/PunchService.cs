using Task2.DTOs;
using Task2.Model;
using Task2.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Task2.Services.PunchService;

namespace Task2.Services.PunchService
{
    public class PunchService : IPunchService
    {
        private readonly appDbContext _context;

        public PunchService(appDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RecordPunchAsync(PunchDto dto)
        {
            var assignedShift = await _context.EmployeeShifts
                .Where(s => s.EmployeeId == dto.EmployeeId &&
                            dto.PunchTime.Date >= s.EffectiveFrom.Date &&
                            dto.PunchTime.Date <= s.EffectiveTo.Date)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync();

            var shiftStart = dto.PunchTime.Date + assignedShift.Shift.StartTime;
            var status = dto.PunchTime > shiftStart ? "Late" : "OnTime";

            var isGeoValid = await CheckLocationWithinFenceAsync(new LocationRequest { Lat = dto.Latitude ?? 0, Lon = dto.Longitude ?? 0 });

            var punch = new AttendancePunch
            {
                EmployeeId = dto.EmployeeId,
                PunchTime = dto.PunchTime,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsGeoValidated = isGeoValid,
                Status = status
            };

            _context.AttendancePunches.Add(punch);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> CheckLocationWithinFenceAsync(LocationRequest request)
        {
            const double officeLat = 11.2588;
            const double officeLon = 75.7804;
            var distance = CalculateDistance(request.Lat, request.Lon, officeLat, officeLon);
            return Task.FromResult(distance <= 200);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371000; // meters
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);
    }
}