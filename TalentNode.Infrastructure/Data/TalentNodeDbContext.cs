using Microsoft.EntityFrameworkCore;
using TalentNode.Domain.Entities;

namespace TalentNode.Infrastructure.Data
{
    public class TalentNodeDbContext(DbContextOptions<TalentNodeDbContext> Options):DbContext(Options)
    {
        public DbSet<EmployeEntity> Employees { get; set; } 
    }
}
