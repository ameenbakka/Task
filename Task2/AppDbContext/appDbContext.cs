using Microsoft.EntityFrameworkCore;
using Task2.Model;
namespace Task2.AppDbContext
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public DbSet<AttendancePunch> AttendancePunches { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
