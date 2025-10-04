    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentNode.Domain.Entities;
using TalentNode.Domain.Models;

namespace TalentNode.Domain.interfaces
{
    public interface IEmployeeRepository
    {
        public  Task<IEnumerable<EmployeEntity>> GetEmployees();
        public Task<EmployeEntity> AddEmployeeAsync(EmployeEntity EmployeEntity);
        public Task<List<Get_All_Employee_Data>> Get_All_Employee_Data();

        public  Task<DocumentDetails> GetDocumentByID(int EmployeeID);

        public Task<int> AddExperienceAsync(ExperienceModel EmployeEntity);

    }
}
