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

        public async Task<int> AddEmployeeAsync(SaveEmployee model)
        {
            if (model == null || model.EmpId <= 0)
                return 0;

            // Fetch the employee record
            var employee = dbContext.Employee.FirstOrDefault(e => e.EmployeeID == model.EmpId);
            if (employee == null)
                return 0;


            // Update only the requested fields
            employee.WorkingLocation = model.Location ?? employee.WorkingLocation;
            employee.StateID = model.State != 0 ? model.State : employee.StateID;
            employee.DistrictID = model.District != 0 ? model.District : employee.DistrictID;
            employee.CurrentPosition = model.CurrentPosition ?? employee.CurrentPosition;
            employee.CurrentSalary = model.CurrentSallary?.ToString() ?? employee.CurrentSalary;
            employee.ExpectedSalary = model.ExpectedSallary?.ToString() ?? employee.ExpectedSalary;
            employee.ResumeID = employee.ResumeID; // not updating
            employee.EmpImageID = employee.EmpImageID; // not updating
            employee.Experience = employee.Experience; // not updating
            employee.Email = employee.Email; // not updating
            employee.Phone = employee.Phone; // not updating
            employee.WorkingLocation = model.Location ?? employee.WorkingLocation; // map to company if stored there
            employee.Professional_Summary = model.Bio ?? employee.Professional_Summary;
            dbContext.Employee.Update(employee);
            // Save changes
           return dbContext.SaveChanges();

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
        public async Task<int> AddExperienceAsync(List<ExperienceModel> EmployeEntity)
        {
            if (EmployeEntity == null)
                return 0;
            dbContext.EmployeeExperiences.RemoveRange(dbContext.EmployeeExperiences.Where(x => x.EmployeeID == EmployeEntity[0].EmployeeID));


            foreach (var EE in EmployeEntity)
            {
                
                //if (dbContext.Experience.Where(x => x.ExperienceID == EE.ExperienceId).FirstOrDefault() != null)
                //{
                //    var record = dbContext.Experience.Where(x=>x.ExperienceID==EE.ExperienceId).FirstOrDefault();
                //    record.OrganizationName = EE.Company;
                //    record.FromDate = EE.StartDate;
                //    record.ToDate = EE.EndDate;
                //    record.workDescription = EE.Description;
                //    //record.CreatedOn = DateTime.Now;
                //    record.UpdatedOn = DateTime.Now;
                //    record.position = EE.Position;
                //    dbContext.Experience.Update(record);
                //    //var emp = dbContext.EmployeeExperiences.Where(x => x.EmployeeID == EE.EmployeeID).FirstOrDefault();
                //    //emp.EmployeeID = EE.EmployeeID;
                //    //emp.ExperienceID = record.ExperienceID;
                //    //dbContext.EmployeeExperiences.Add(emp);
                //}
                //else
                //{
                    Experience record = new Experience();
                    record.OrganizationName = EE.Company;
                    record.FromDate = EE.StartDate;
                    record.ToDate = EE.EndDate ;
                    record.CreatedOn = DateTime.Now;
                    record.workDescription = EE.Description;
                    //record.CreatedOn = EE.Created_On;
                    //record.UpdatedOn = EE.Updated_On;
                    record.position = EE.Position;
                    dbContext.Experience.Add(record);
                    dbContext.SaveChanges();
                    EmployeeExperiences emp = new EmployeeExperiences();
                    emp.EmployeeID = EE.EmployeeID;
                    emp.ExperienceID = record.ExperienceID;
                    dbContext.EmployeeExperiences.Add(emp);
                   
                //}
 
            }
            
            //dbContext.SaveChanges();
            
            return dbContext.SaveChanges();
        }
        public async Task<int> AddEducationAsync(List<EducationModel> EmployeEntity)
        {
            if (EmployeEntity == null)
            {
                return 0;
            }
            dbContext.EmployeeQualification.RemoveRange(dbContext.EmployeeQualification.Where(x=>x.EmpID== EmployeEntity[0].degEmpId));

            List<EmployeeQualification> eq = new List<EmployeeQualification>();
            foreach (var item in EmployeEntity)
            {
                //if(dbContext.EmployeeQualification.Where(x=>x.QualID==item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault() == null)
                //{
                    EmployeeQualification record = new EmployeeQualification();
                    record.EmpID = item.degEmpId;
                    record.QualID =Convert.ToInt32(item.degree);
                    record.Institute = item.Institution;
                    record.PassingYesr = item.Year.ToString();
                record.Percentage_CGPA = item.Percentage;
                    dbContext.EmployeeQualification.Add(record);
                //}
                //else
                //{
                //    var record = dbContext.EmployeeQualification.Where(x => x.QualID == item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault();
                //    record.Institute = item.Institution;
                //    record.PassingYesr = item.Year;
                //    dbContext.EmployeeQualification.Update(record);
                //}

            }
           // dbContext.EmployeeQualification.AddRange(eq);
            return dbContext.SaveChanges();

        }


        public async Task<int> AddskillsAsync(List<SkillAdd> EmployeEntity)
        {
            if (EmployeEntity == null)
            {
                return 0;
            }
            dbContext.EmployeeSkill.RemoveRange(dbContext.EmployeeSkill.Where(x => x.EmployeeID == EmployeEntity[0].skillEmpId));

            List<EmployeeQualification> eq = new List<EmployeeQualification>();
            foreach (var item in EmployeEntity)
            {
                //if(dbContext.EmployeeQualification.Where(x=>x.QualID==item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault() == null)
                //{
                EmployeeSkill record = new EmployeeSkill();
                record.SkillID = item.name;
                record.EmployeeID = Convert.ToInt32(item.skillEmpId);
                record.level = item.level;
                
                dbContext.EmployeeSkill.Add(record);
                //}
                //else
                //{
                //    var record = dbContext.EmployeeQualification.Where(x => x.QualID == item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault();
                //    record.Institute = item.Institution;
                //    record.PassingYesr = item.Year;
                //    dbContext.EmployeeQualification.Update(record);
                //}

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
                          degEmpId= employee.EmployeeID,
                          Degree = q.QualificationID ,
                          Institution = eq.Institute ?? "",
                          Year = Convert.ToInt32(eq.PassingYesr ?? "0"),
                          Percentage = (double)eq.Percentage_CGPA,
                      }).ToList();

            var experience = dbContext.EmployeeExperiences
                .Where(exMap => exMap.EmployeeID == employee.EmployeeID)
                .Join(dbContext.Experience,
                      exMap => exMap.ExperienceID,
                      ex => ex.ExperienceID,
                      (exMap, ex) => new ExperienceDetail
                      {
                          EmployeeID=exMap.EmployeeID,
                          ExperienceId=ex.ExperienceID,
                          Company = ex.OrganizationName ?? "",
                          Position = ex.position ?? "",
                          StartDate = ex.FromDate.ToString("yyyy-MM"),
                          EndDate = ex.ToDate != DateTime.MinValue ? ex.ToDate.ToString("yyyy-MM") : "",
                          Current = ex.ToDate == DateTime.MinValue,
                          Description = ex.workDescription
                      }).ToList();

            var skills = dbContext.EmployeeSkill
                .Where(es => es.EmployeeID == employee.EmployeeID)
                .Join(dbContext.SkillMaster,
                      es => es.SkillID,
                      sm => sm.SkillID,
                      (es, sm) => new SkillDetail
                      {skillEmpId=es.EmployeeID,
                          Name = sm.SkillID ,
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
                CurrentSalary=employee.CurrentSalary??"",
                Avatar = avatar,
                Resume = resume,
                Bio=employee.Professional_Summary,
                stateid = employee.StateID,
                districtId=employee.DistrictID,
                Location = employee.WorkingLocation,//districtName + ", " + stateName,
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