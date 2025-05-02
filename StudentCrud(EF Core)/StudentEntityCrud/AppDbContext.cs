using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using StudentEntityCrud.Models;

namespace StudentEntityCrud
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
