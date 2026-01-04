using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Domain.Models.TalentNode.Domain.Models;
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

        public async Task<int> AddDocumentAsync(EmployeeDocumentModel model)
        {
            if (model == null || model.EmployeeID <= 0)
                return 0;

            // Fetch the employee record
            if (model.Mode == "R")
            {

                var employee = dbContext.Employee.Where(x => x.EmployeeID == model.EmployeeID).FirstOrDefault();
                if (employee == null)
                {
                    return 0;
                }
                var docavail = dbContext.Document.Where(x => x.DocumentID == employee.ResumeID).FirstOrDefault();
                if (docavail != null)
                {
                    docavail.IsRemoved = true;
                    dbContext.Document.Update(docavail);
                }
                Document doc = new Document();
                doc.UploadDate = DateTime.UtcNow;
                doc.UpdatedBy = "Employee";
                doc.FileContentBase64 = model.FileContentBase64;
                doc.DocName = model.DocName;
                doc.FileName = model.FileName;
                doc.CreatedBy = "Employee";
                doc.FileType = model.FileType;
                doc.Link = "";
                dbContext.Document.Add(doc);
                dbContext.SaveChanges();
                employee.ResumeID = doc.DocumentID;
                dbContext.Employee.Update(employee);

            }
            if (model.Mode == "P")
            {

                var employee = dbContext.Employee.Where(x => x.EmployeeID == model.EmployeeID).FirstOrDefault();
                if (employee == null)
                {
                    return 0;
                }
                var docavail = dbContext.Document.Where(x => x.DocumentID == employee.EmpImageID).FirstOrDefault();
                if (docavail != null)
                {
                    docavail.IsRemoved = true;
                    dbContext.Document.Update(docavail);
                }
                Document doc = new Document();
                doc.UploadDate = DateTime.UtcNow;
                doc.UpdatedBy = "Employee";
                doc.FileContentBase64 = model.FileContentBase64;
                doc.DocName = model.DocName;
                doc.FileName = model.FileName;
                doc.FileType = model.FileType;
                doc.CreatedBy = "Employee";
                doc.Link = "";
                dbContext.Document.Add(doc);
                dbContext.SaveChanges();
                employee.EmpImageID = doc.DocumentID;
                dbContext.Employee.Update(employee);

            }

            // Save changes
            return dbContext.SaveChanges();

        }
        public async Task<List<Get_All_Employee_Data>> Get_All_Employee_Data()
        {
            var employeeDetails = (from e in dbContext.Employee
                                   join d in dbContext.DistrictMaster on e.DistrictID equals d.DistrictID
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
                        .Where(es => es.DocumentID == e.EmpImageID && es.IsRemoved == false)
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
                                  where d.IsRemoved == false
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
                record.ToDate = EE.EndDate;
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
            dbContext.EmployeeQualification.RemoveRange(dbContext.EmployeeQualification.Where(x => x.EmpID == EmployeEntity[0].degEmpId));

            List<EmployeeQualification> eq = new List<EmployeeQualification>();
            foreach (var item in EmployeEntity)
            {
                //if(dbContext.EmployeeQualification.Where(x=>x.QualID==item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault() == null)
                //{
                EmployeeQualification record = new EmployeeQualification();
                record.EmpID = item.degEmpId;
                record.QualID = item.degree;
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
        public async Task<int> AddKeyskillsAsync(List<AddEmployeeKeyskillmodel> EmployeEntity)
        {
            if (EmployeEntity == null)
            {
                return 0;
            }
            dbContext.EmployeeKeySkills.RemoveRange(dbContext.EmployeeKeySkills.Where(x => x.EmpId == EmployeEntity[0].EmpId));

            List<EmployeeQualification> eq = new List<EmployeeQualification>();
            foreach (var item in EmployeEntity)
            {
                //if(dbContext.EmployeeQualification.Where(x=>x.QualID==item.QualificationID && x.EmpID == item.EmployeeID).FirstOrDefault() == null)
                //{
                EmployeeKeySkills record = new EmployeeKeySkills();
                record.KeySkillId = item.KeySkillId;
                record.EmpId = Convert.ToInt32(item.EmpId);
                record.level = item.level;

                dbContext.EmployeeKeySkills.Add(record);
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
public async Task<int> AddProjectsAsync(List<ProjectAdd> projectEntity)
{
    if (projectEntity == null || projectEntity.Count == 0)
    {
        return 0;
    }

    using var transaction = await dbContext.Database.BeginTransactionAsync();

    try
    {
        int empId = projectEntity[0].projectEmpId;

        // Remove old mappings
        var oldEmpProjects = dbContext.EmployeeProjects
                                      .Where(x => x.EmpId == empId);

        dbContext.EmployeeProjects.RemoveRange(oldEmpProjects);

        foreach (var item in projectEntity)
        {
            // 1️⃣ Insert into Project table
            Projects project = new Projects
            {
                ProjectName = item.name,
                StartDate = DateOnly.FromDateTime(DateTime.ParseExact(
                    item.startDate + "-01",
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                )),
                EndDate = (item.ongoing == true ? null : DateOnly.FromDateTime(DateTime.ParseExact(
                    item.endDate + "-01",
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture
                ))),
                Description = item.description,
                Technologies = item.technologies,
                ProjectUrl = item.url,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync(); // Needed to get ProjectId

            // 2️⃣ Insert mapping into EmployeeProject
            EmployeeProjects empProject = new EmployeeProjects
            {
                EmpId = empId,
                ProjectId = project.ProjectId,
                iscurrentlyworking = item.ongoing
            };

            dbContext.EmployeeProjects.Add(empProject);
        }

        int result = await dbContext.SaveChangesAsync();

        await transaction.CommitAsync();   // ✅ commit
        return result;
    }
    catch
    {
        await transaction.RollbackAsync(); // 🔥 rollback
        throw;
    }
}

        //    public async Task<UserProfile> GetEmployeeDetails(int emplyeeid)
        //    {

        //        var employee = dbContext.Employee.FirstOrDefault(e => e.EmployeeID == emplyeeid);
        //        if (employee == null) return null;

        //        var avatar = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.EmpImageID)?.FileContentBase64 ?? "";
        //        var resume = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.ResumeID)?.FileContentBase64 ?? "";
        //        var districtName = dbContext.DistrictMaster.FirstOrDefault(d => d.DistrictID == employee.DistrictID)?.DistrictName ?? "";
        //        var stateName = dbContext.StateMaster.FirstOrDefault(s => s.StateID == employee.StateID)?.StateName ?? "";

        //        var education = dbContext.EmployeeQualification
        //            .Where(eq => eq.EmpID == employee.EmployeeID)
        //            .Join(dbContext.QualificationMaster,
        //                  eq => eq.QualID,
        //                  q => q.QualificationID,
        //                  (eq, q) => new EducationDetail
        //                  {
        //                      degEmpId = employee.EmployeeID,
        //                      Degree = q.QualificationID,
        //                      Institution = eq.Institute ?? "",
        //                      Year = Convert.ToInt32(eq.PassingYesr ?? "0"),
        //                      Percentage = (double)eq.Percentage_CGPA,
        //                  }).ToList();

        //        var experience = dbContext.EmployeeExperiences
        //            .Where(exMap => exMap.EmployeeID == employee.EmployeeID)
        //            .Join(dbContext.Experience,
        //                  exMap => exMap.ExperienceID,
        //                  ex => ex.ExperienceID,
        //                  (exMap, ex) => new ExperienceDetail
        //                  {
        //                      EmployeeID = exMap.EmployeeID,
        //                      ExperienceId = ex.ExperienceID,
        //                      Company = ex.OrganizationName ?? "",
        //                      Position = ex.position ?? "",
        //                      StartDate = ex.FromDate.ToString("yyyy-MM"),
        //                      EndDate = ex.ToDate != DateTime.MinValue ? ex.ToDate.ToString("yyyy-MM") : "",
        //                      Current = ex.ToDate == DateTime.MinValue,
        //                      Description = ex.workDescription
        //                  }).ToList();

        //        var skills = dbContext.EmployeeSkill
        //            .Where(es => es.EmployeeID == employee.EmployeeID)
        //            .Join(dbContext.SkillMaster,
        //                  es => es.SkillID,
        //                  sm => sm.SkillID,
        //                  (es, sm) => new SkillDetail
        //                  {
        //                      skillEmpId = es.EmployeeID,
        //                      Name = sm.SkillID,
        //                      Level = es.level ?? ""
        //                  }).ToList();

        //        var keySkills = dbContext.EmployeeKeySkills
        //            .Where(es => es.EmpId == employee.EmployeeID)
        //            .Join(dbContext.MdKeySkill,
        //                  es => es.KeySkillId,
        //                  sm => sm.keyskillId,
        //                  (es, sm) => new SkillKeyDetail
        //                  {
        //                      EmpId = es.EmpId,
        //                      KeySkillId = sm.keyskillId,
        //                      level = es.level ?? ""
        //                  }).ToList();

        //        // build final UserProfile

        //        var projects = dbContext.EmployeeProjects
        //.Where(ep => ep.EmpId == employee.EmployeeID)
        //.Join(dbContext.Projects,
        //      ep => ep.ProjectId,
        //      p => p.ProjectId,
        //      (ep, p) => new ProjectDetail
        //      {
        //          ProjectEmpId = ep.EmpId,
        //          name = p.ProjectName ?? "",
        //          StartDate = p.StartDate.Value.ToString("yyyy-MM"),
        //          EndDate = ep.iscurrentlyworking==true
        //                        ? ""
        //                        : p.EndDate.Value.ToString("yyyy-MM"),
        //          ongoing = ep.iscurrentlyworking,
        //          Description = p.Description ?? "",
        //          Technologies = p.Technologies ?? "",
        //          Url = p.ProjectUrl ?? ""
        //      })
        //.ToList();
        //        var userProfile = new UserProfile
        //        {
        //            Id = employee.EmployeeID,
        //            FirstName = employee.FirstName ?? "",
        //            LastName = employee.LastName ?? "",
        //            Email = employee.Email ?? "",
        //            Phone = employee.Phone ?? "",
        //            CurrentPosition = employee.CurrentPosition ?? "",
        //            CurrentCompany = employee.WorkingLocation ?? "",
        //            ExpectedSalary = decimal.TryParse(employee.ExpectedSalary ?? "0", out var sal) ? sal : 0,
        //            CurrentSalary = employee.CurrentSalary ?? "",
        //            Avatar = avatar,
        //            Resume = resume,
        //            Bio = employee.Professional_Summary,
        //            stateid = employee.StateID,
        //            districtId = employee.DistrictID,
        //            Location = employee.WorkingLocation,//districtName + ", " + stateName,
        //            Education = education,
        //            Experience = experience,
        //            Skills = skills,
        //            KeySkills = keySkills,
        //            Projects= projects,
        //            Languages = new List<string> { "English", "Hindi" },
        //            SocialLinks = new SocialLinks { Linkedin = "", Github = "", Portfolio = "" }
        //        };
        //        return userProfile;

        //    }

        public async Task<UserProfile> GetEmployeeDetails(int emplyeeid)
        {
            dbContext.Database.SetCommandTimeout(180);

            var employee = await dbContext.Employee
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeID == emplyeeid);

            if (employee == null)
                return null;

            // Load avatar & resume in single query
            var documents = await dbContext.Document
                .AsNoTracking()
                .Where(d => d.DocumentID == employee.EmpImageID
                         || d.DocumentID == employee.ResumeID)
                .ToListAsync();

            var avatar = documents
                .FirstOrDefault(d => d.DocumentID == employee.EmpImageID)
                ?.FileContentBase64 ?? "";

            var resume = documents
                .FirstOrDefault(d => d.DocumentID == employee.ResumeID)
                ?.FileContentBase64 ?? "";

            var education = await dbContext.EmployeeQualification
                .AsNoTracking()
                .Where(eq => eq.EmpID == employee.EmployeeID)
                .Join(dbContext.QualificationMaster.AsNoTracking(),
                      eq => eq.QualID,
                      q => q.QualificationID,
                      (eq, q) => new EducationDetail
                      {
                          degEmpId = employee.EmployeeID,
                          Degree = q.QualificationID,
                          Institution = eq.Institute ?? "",
                          Year = Convert.ToInt32(eq.PassingYesr ?? "0"),
                          Percentage = (double)eq.Percentage_CGPA
                      })
                .ToListAsync();

            var experience = await dbContext.EmployeeExperiences
                .AsNoTracking()
                .Where(exMap => exMap.EmployeeID == employee.EmployeeID)
                .Join(dbContext.Experience.AsNoTracking(),
                      exMap => exMap.ExperienceID,
                      ex => ex.ExperienceID,
                      (exMap, ex) => new ExperienceDetail
                      {
                          EmployeeID = exMap.EmployeeID,
                          ExperienceId = ex.ExperienceID,
                          Company = ex.OrganizationName ?? "",
                          Position = ex.position ?? "",
                          StartDate = ex.FromDate.ToString("yyyy-MM"),
                          EndDate = ex.ToDate != DateTime.MinValue
                                        ? ex.ToDate.ToString("yyyy-MM")
                                        : "",
                          Current = ex.ToDate == DateTime.MinValue,
                          Description = ex.workDescription
                      })
                .ToListAsync();

            var skills = await dbContext.EmployeeSkill
                .AsNoTracking()
                .Where(es => es.EmployeeID == employee.EmployeeID)
                .Join(dbContext.SkillMaster.AsNoTracking(),
                      es => es.SkillID,
                      sm => sm.SkillID,
                      (es, sm) => new SkillDetail
                      {
                          skillEmpId = es.EmployeeID,
                          Name = sm.SkillID,
                          Level = es.level ?? "",
                          skillname=sm.SkillName
                      })
                .ToListAsync();

            var keySkills = await dbContext.EmployeeKeySkills
                .AsNoTracking()
                .Where(es => es.EmpId == employee.EmployeeID)
                .Join(dbContext.MdKeySkill.AsNoTracking(),
                      es => es.KeySkillId,
                      sm => sm.keyskillId,
                      (es, sm) => new SkillKeyDetail
                      {
                          EmpId = es.EmpId,
                          KeySkillId = sm.keyskillId,
                          level = es.level ?? "",
                          keyName=sm.Description
                      })
                .ToListAsync();

            var projects = await dbContext.EmployeeProjects
                .AsNoTracking()
                .Where(ep => ep.EmpId == employee.EmployeeID)
                .Join(dbContext.Projects.AsNoTracking(),
                      ep => ep.ProjectId,
                      p => p.ProjectId,
                      (ep, p) => new ProjectDetail
                      {
                          ProjectEmpId = ep.EmpId,
                          name = p.ProjectName ?? "",
                          StartDate = p.StartDate.HasValue
                                        ? p.StartDate.Value.ToString("yyyy-MM")
                                        : "",
                          EndDate = ep.iscurrentlyworking == true
                                        ? ""
                                        : (p.EndDate.HasValue
                                            ? p.EndDate.Value.ToString("yyyy-MM")
                                            : ""),
                          ongoing = ep.iscurrentlyworking,
                          Description = p.Description ?? "",
                          Technologies = p.Technologies ?? "",
                          Url = p.ProjectUrl ?? ""
                      })
                .ToListAsync();

            return new UserProfile
            {
                Id = employee.EmployeeID,
                FirstName = employee.FirstName ?? "",
                LastName = employee.LastName ?? "",
                Email = employee.Email ?? "",
                Phone = employee.Phone ?? "",
                CurrentPosition = employee.CurrentPosition ?? "",
                CurrentCompany = employee.WorkingLocation ?? "",
                ExpectedSalary = decimal.TryParse(employee.ExpectedSalary ?? "0", out var sal) ? sal : 0,
                CurrentSalary = employee.CurrentSalary ?? "",
                Avatar = avatar,
                Resume = resume,
                Bio = employee.Professional_Summary,
                stateid = employee.StateID,
                districtId = employee.DistrictID,
                Location = employee.WorkingLocation,
                Education = education,
                Experience = experience,
                Skills = skills,
                KeySkills = keySkills,
                Projects = projects,
                Languages = new List<string> { "English", "Hindi" },
                SocialLinks = new SocialLinks()
            };
        }


        public async Task<List<ApplicantProfile>> GetApplicantProfile(ApplicantModel ammd)
        {
            int totalRecords = await dbContext.JobApplicationCandidate.Where(jb => jb.JobId == ammd.JobId)
    .CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalRecords / ammd.PageSize);
            List<ApplicantProfile> result = new List<ApplicantProfile>();
            var data = dbContext.JobApplicationCandidate
                       .Where(jb => jb.JobId == ammd.JobId)
                       .OrderBy(jb => jb.CandidateId) // ensure consistent ordering
                       .Skip((ammd.PageNumber - 1) * ammd.PageSize)
                       .Take(ammd.PageSize)
                       .Select(jb => jb.CandidateId)
                       .ToList();
            foreach (var emplyeeid in data)
            {
                ApplicantProfile temp = new ApplicantProfile();
                var employee = dbContext.Employee.FirstOrDefault(e => e.EmployeeID == emplyeeid);
                if (employee == null) return null;

                var avatar = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.EmpImageID)?.FileContentBase64 ?? "";
                var resume = dbContext.Document.FirstOrDefault(d => d.DocumentID == employee.ResumeID)?.FileContentBase64 ?? "";
                var districtName = dbContext.DistrictMaster.FirstOrDefault(d => d.DistrictID == employee.DistrictID)?.DistrictName ?? "";
                var stateName = dbContext.StateMaster.FirstOrDefault(s => s.StateID == employee.StateID)?.StateName ?? "";

                var education = dbContext.EmployeeQualification
                    .Where(eq => eq.EmpID == employee.EmployeeID)
                    .Join(dbContext.QualificationMaster,
                          eq => eq.QualID,
                          q => q.QualificationID,
                          (eq, q) => new EducationDetail
                          {
                              degEmpId = employee.EmployeeID,
                              Degree = q.QualificationID,
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
                              EmployeeID = exMap.EmployeeID,
                              ExperienceId = ex.ExperienceID,
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
                          (es, sm) => new SkillDetailApplicant
                          {
                              skillEmpId = es.EmployeeID,
                              Name = sm.SkillName,
                              Level = es.level ?? "",
                              skillname=sm.SkillName
                          }).ToList();
                var ApplicantStatusId = dbContext.JobApplicationCandidate.Where(X => X.CandidateId == emplyeeid)?.FirstOrDefault()?.Status;
                var ApplicantStatusDescription = dbContext.CandidateStatusMaster.Where(X => X.StatusId == int.Parse(ApplicantStatusId))?.FirstOrDefault()?.StatusName;
                var Applydate = dbContext.JobApplicationCandidate.Where(X => X.CandidateId == emplyeeid)?.FirstOrDefault()?.AppliedDate;
                // build final UserProfile
                var userProfile = new ApplicantProfile
                {
                    Id = employee.EmployeeID,
                    FirstName = employee.FirstName ?? "",
                    LastName = employee.LastName ?? "",
                    Email = employee.Email ?? "",
                    Phone = employee.Phone ?? "",
                    CurrentPosition = employee.CurrentPosition ?? "",
                    CurrentCompany = employee.WorkingLocation ?? "",
                    ExpectedSalary = decimal.TryParse(employee.ExpectedSalary ?? "0", out var sal) ? sal : 0,
                    CurrentSalary = employee.CurrentSalary ?? "",
                    Avatar = avatar,
                    Resume = resume,
                    Bio = employee.Professional_Summary,
                    stateid = employee.StateID,
                    districtId = employee.DistrictID,
                    Location = employee.WorkingLocation,//districtName + ", " + stateName,
                    Education = education,
                    Experience = experience,
                    Skills = skills,
                    Languages = new List<string> { "English", "Hindi" },
                    SocialLinks = new SocialLinks { Linkedin = "", Github = "", Portfolio = "" },
                    ApplicantStatus = ApplicantStatusDescription,
                    ApplicantStatusID = int.Parse(ApplicantStatusId),
                    ApplyDate = Applydate,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages

                };

                result.Add(userProfile);
            }
            return result.ToList();

        }

    }
}