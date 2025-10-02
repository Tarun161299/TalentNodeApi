using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class EmployeeRepository(TalentNodeDbContext dbContext) : IEmployeeRepository
    {
        
        public async Task<IEnumerable<EmployeEntity>> GetEmployees()
        {
            return await dbContext.SignupDetails.ToListAsync();
        }

        public async Task<EmployeEntity> AddEmployeeAsync(EmployeEntity EmployeEntity)
        {
            EmployeEntity.Id = Guid.NewGuid();
            dbContext.SignupDetails.Add(EmployeEntity);
            await dbContext.SaveChangesAsync();
            return EmployeEntity;

        }
        public async Task<List<Get_All_Employee_Data>> Get_All_Employee_Data()
        {
            var employeeDetails = (from e in dbContext.Employee
                                   join d in dbContext.DistrictMaster on e.EmployeeID equals d.DistrictID
                                   join s in dbContext.StateMaster on e.StateID equals s.StateID

                                   select new Get_All_Employee_Data
                                   {
                                       EmployeeID = e.EmployeeID,
                                       StateName = s.StateName,
                                       DistrictName = d.DistrictName,
                                       Experience = e.Experience,
                                       FirstName=e.FirstName,
                                       LastName=e.LastName,
                                       Email=e.Email,
                                       Phone=e.Phone,
                                       EmpImage = dbContext.Document
                        .Where(es => es.DocumentID == e.EmpImageID)
                        .Select(es => es.FileContentBase64)   // string or byte[]
                        .FirstOrDefault(),
                                       Emp_Skills = (from es in dbContext.EmployeeSkill
                                                     join sm in dbContext.SkillMaster on es.SkillID equals sm.SkillID
                                                     where es.EmployeeID == e.EmployeeID
                                                     select new Skills
                                                     {
                                                         SkillID = sm.SkillID,
                                                         SkillName = sm.SkillName,
                                                     }).ToList(),

                                       Emp_Qualification = (from eq in dbContext.EmployeeQualification
                                                            join qm in dbContext.QualificationMaster on eq.QualificationID equals qm.QualificationID
                                                            where eq.EmployeeID == e.EmployeeID
                                                            select new Qualification { QualificationId = eq.QualificationID, QualificationName = qm.QualificationName }).ToList()
                                   }).ToList();
            return employeeDetails;

        }


        public async Task<DocumentDetails> GetDocumentByID(int EmployeeID)
        {
            //var emplyeelist = await dbContext.Employees;
            var document = await (from emp in dbContext.Employee
                                  join d in dbContext.Document
                                  on emp.ResumeID equals d.DocumentID
                                  select new DocumentDetails
                                  {
                                      DocumentID = d.DocumentID,
                                      EmployeeID = emp.EmployeeID,//d.EmployeeID,
                                      DocName = d.DocName,
                                      FileName = d.FileName,
                                      FileType = d.FileType,
                                      FileContentBase64 = d.FileContentBase64,
                                      Link = d.Link,
                                      UploadDate = d.UploadDate,
                                      IsRemoved = d.IsRemoved,
                                      CreatedBy = d.CreatedBy,
                                      UpdatedBy = d.UpdatedBy
                                  })
    .FirstOrDefaultAsync();   // 👈 only one record

            return document;




        }
    }
}
