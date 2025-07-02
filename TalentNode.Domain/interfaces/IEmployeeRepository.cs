    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;

namespace TalentNode.Domain.interfaces
{
    public interface IEmployeeRepository
    {
        public  Task<IEnumerable<EmployeEntity>> GetEmployees();
        public Task<EmployeEntity> AddEmployeeAsync(EmployeEntity EmployeEntity);
    }
}
