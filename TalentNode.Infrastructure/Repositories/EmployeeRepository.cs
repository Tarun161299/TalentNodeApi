using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class EmployeeRepository(TalentNodeDbContext dbContext) : IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeEntity>> GetEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeEntity> AddEmployeeAsync(EmployeEntity EmployeEntity)
        {
            EmployeEntity.Id = Guid.NewGuid();
              dbContext.Employees.Add(EmployeEntity);
            await dbContext.SaveChangesAsync();
            return EmployeEntity;

        }
    }
}
