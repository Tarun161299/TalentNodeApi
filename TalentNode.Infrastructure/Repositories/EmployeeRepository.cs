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
using TalentNode.Domain.Models.YourNamespace.Models;
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
                                       FirstName = e.FirstName,
                                       LastName = e.LastName,
                                       Email = e.Email,
                                       Phone = e.Phone,
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

                                       //Emp_Qualification = (from eq in dbContext.EmployeeQualification
                                       //                     join qm in dbContext.QualificationMaster on eq.QualID equals qm.QualificationID
                                       //                     where eq.EmpID == e.EmployeeID
                                       //                     select new Qualification { QualificationId = eq.QualID, QualificationName = qm.QualificationName }).ToList()
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
        public async Task<int> AddExperienceAsync(ExperienceModel EmployeEntity)
        {
            Experience exp = new Experience();
            exp.OrganizationName = EmployeEntity.Company;
            exp.FromDate = EmployeEntity.StartDate;
            exp.ToDate = EmployeEntity.EndDate;
            exp.CreatedOn = EmployeEntity.Created_On;
            exp.UpdatedOn = EmployeEntity.Updated_On;
            dbContext.Experience.Add(exp);
            dbContext.SaveChanges();
            EmployeeExperiences emp = new EmployeeExperiences();
            emp.EmployeeID = EmployeEntity.EmployeeID;
            emp.ExperienceID = exp.ExperienceID;
            dbContext.EmployeeExperiences.Add(emp);
            return dbContext.SaveChanges();
        }
        public async Task<int> AddEducationAsync(List<EducationModel> EmployeEntity)
        {
            List<EmployeeQualification> eq = new List<EmployeeQualification>();
            foreach (var item in EmployeEntity)
            {
                EmployeeQualification record = new EmployeeQualification();
                record.EmpID = item.EmployeeID;
                record.QualID = item.QualificationID;
                record.Institute = item.Institution;
                record.PassingYesr = item.Year;
                eq.Add(record);
            }
           // dbContext.EmployeeQualification.AddRange(eq);
            return dbContext.SaveChanges();

        }

        public async Task<UserProfile> GetEmployeeDetails(int emplyeeid)
        {

            var employee = dbContext.Employee.FirstOrDefault(e => e.EmployeeID == emplyeeid);
            if (employee == null) return null;

            var avatar = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.EmpImageID)?.Link ?? "";
            var resume = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.ResumeID)?.Link ?? "";
            var districtName = dbContext.DistrictMaster.FirstOrDefault(d => d.DistrictID == employee.DistrictID)?.DistrictName ?? "";
            var stateName = dbContext.StateMaster.FirstOrDefault(s => s.StateID == employee.StateID)?.StateName ?? "";

            var education = dbContext.EmployeeQualification
                .Where(eq => eq.EmpID == employee.EmployeeID)
                .Join(dbContext.QualificationMaster,
                      eq => eq.QualID,
                      q => q.QualificationID,
                      (eq, q) => new EducationDetail
                      {
                          Degree = q.QualificationName ?? "",
                          Institution = eq.Institute ?? "",
                          Year = Convert.ToInt32(eq.PassingYesr ?? "0"),
                          Percentage = 0
                      }).ToList();

            var experience = dbContext.EmployeeExperiences
                .Where(exMap => exMap.EmployeeID == employee.EmployeeID)
                .Join(dbContext.Experience,
                      exMap => exMap.ExperienceID,
                      ex => ex.ExperienceID,
                      (exMap, ex) => new ExperienceDetail
                      {
                          Company = ex.OrganizationName ?? "",
                          Position = employee.CurrentPosition ?? "",
                          StartDate = ex.FromDate.ToString("yyyy-MM"),
                          EndDate = ex.ToDate != DateTime.MinValue ? ex.ToDate.ToString("yyyy-MM") : "",
                          Current = ex.ToDate == DateTime.MinValue,
                          Description = ""
                      }).ToList();

            var skills = dbContext.EmployeeSkill
                .Where(es => es.EmployeeID == employee.EmployeeID)
                .Join(dbContext.SkillMaster,
                      es => es.SkillID,
                      sm => sm.SkillID,
                      (es, sm) => new SkillDetail
                      {
                          Name = sm.SkillName ?? "",
                          Level = es.level ?? ""
                      }).ToList();

            // build final UserProfile
            var userProfile = new UserProfile
            {
                Id = employee.EmployeeID,
                FirstName = employee.FirstName ?? "",
                LastName = employee.LastName ?? "",
                Email = employee.Email ?? "",
                Phone = employee.Phone ?? "",
                CurrentPosition = employee.CurrentPosition ?? "",
                CurrentCompany = employee.WorkingLocation ?? "",
                ExpectedSalary = decimal.TryParse(employee.ExpectedSalary ?? "0", out var sal) ? sal : 0,
                Avatar = avatar,
                Resume = resume,
                Location = districtName + ", " + stateName,
                Education = education,
                Experience = experience,
                Skills = skills,
                Languages = new List<string> { "English", "Hindi" },
                SocialLinks = new SocialLinks { Linkedin = "", Github = "", Portfolio = "" }
            };

            return userProfile;

            //        var employeeData = dbContext.Employee
            //.Where(e => e.EmployeeID == emplyeeid)
            //.Select(e => new
            //{
            //    e.EmployeeID,
            //    e.FirstName,
            //    e.LastName,
            //    e.Email,
            //    e.Phone,
            //    e.CurrentPosition,
            //    e.WorkingLocation,
            //    e.ExpectedSalary,
            //    e.EmpImageID,
            //    e.ResumeID,
            //    e.StateID,
            //    e.DistrictID
            //})
            //.AsEnumerable() // move to in-memory to allow TryParse
            //.Select(e => new UserProfile
            //{
            //    Id = e.EmployeeID,
            //    FirstName = e.FirstName ?? "",
            //    LastName = e.LastName ?? "",
            //    Email = e.Email ?? "",
            //    Phone = e.Phone ?? "",
            //    CurrentPosition = e.CurrentPosition ?? "",
            //    CurrentCompany = e.WorkingLocation ?? "",
            //    ExpectedSalary = decimal.TryParse(e.ExpectedSalary ?? "0", out var sal) ? sal : 0,

            //    // Avatar and Resume
            //    Avatar = dbContext.Document.FirstOrDefault(d => d.DocumentID == e.EmpImageID)?.Link ?? "",
            //    Resume = dbContext.Document.FirstOrDefault(d => d.DocumentID == e.ResumeID)?.Link ?? "",

            //    // Location
            //    Location = (dbContext.DistrictMaster.FirstOrDefault(d => d.DistrictID == e.DistrictID)?.DistrictName ?? "")
            //             + ", "
            //             + (dbContext.StateMaster.FirstOrDefault(s => s.StateID == e.StateID)?.StateName ?? ""),

            //    // Education
            //    Education = dbContext.EmployeeQualification
            //        .Where(eq => eq.EmpID == e.EmployeeID)
            //        .Join(dbContext.QualificationMaster,
            //              eq => eq.QualID,
            //              q => q.QualificationID,
            //              (eq, q) => new EducationDetail
            //              {
            //                  Degree = q.QualificationName ?? "",
            //                  Institution = eq.Institute ?? "",
            //                  Year = Convert.ToInt32(eq.PassingYesr ?? "0"),
            //                  Percentage = 0
            //              }).ToList(),

            //    // Experience
            //    Experience = dbContext.EmployeeExperiences
            //        .Where(exMap => exMap.EmployeeID == e.EmployeeID)
            //        .Join(dbContext.Experience,
            //              exMap => exMap.ExperienceID,
            //              ex => ex.ExperienceID,
            //              (exMap, ex) => new ExperienceDetail
            //              {
            //                  Company = ex.OrganizationName ?? "",
            //                  Position = e.CurrentPosition ?? "",
            //                  StartDate = ex.FromDate.ToString("yyyy-MM"),
            //                  EndDate = ex.ToDate != DateTime.MinValue ? ex.ToDate.ToString("yyyy-MM") : "",
            //                  Current = ex.ToDate == DateTime.MinValue,
            //                  Description = ""
            //              }).ToList(),

            //    // Skills
            //    Skills = dbContext.EmployeeSkill
            //        .Where(es => es.EmployeeID == e.EmployeeID)
            //        .Join(dbContext.SkillMaster,
            //              es => es.SkillID,
            //              sm => sm.SkillID,
            //              (es, sm) => new SkillDetail
            //              {
            //                  Name = sm.SkillName ?? "",
            //                  Level = es.level ?? ""
            //              }).ToList(),

            //    // Languages (placeholder)
            //    Languages = new List<string> { "English", "Hindi" },

            //    // Social links (placeholder)
            //    SocialLinks = new SocialLinks
            //    {
            //        Linkedin = "",
            //        Github = "",
            //        Portfolio = ""
            //    }
            //})
            //.FirstOrDefault();

           // return employeeData;

        }

    }
}