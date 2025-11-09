using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentNode.Domain.Entities;
using TalentNode.Domain.interfaces;
using TalentNode.Domain.Models;
using TalentNode.Infrastructure.Data;

namespace TalentNode.Infrastructure.Repositories
{
    public class JobRepository(TalentNodeDbContext _context) : IJobRepository
    {
        //public async Task<int> SaveJob( JobCreateDto dto)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        JobDetails job=new JobDetails();
        //        bool isUpdate = false;// dto.job.HasValue;

        //        if (!isUpdate)
        //        {
        //            // Create new job
        //            job = new JobDetails
        //            {
        //                JobTitle = dto.Title,
        //                CompanyID = dto.Company,
        //                DepartmentId = dto.Department,
        //                Address = dto.Location,
        //                jobTypeId = dto.Type,
        //                JobLink=dto.ApplicationLink,
        //                NoOfOpenings = dto.Vacancies,
        //                MinimumSalary = dto.SalaryMin.ToString(),
        //                MaximumSalary = dto.SalaryMax.ToString(),
        //                Currency = dto.SalaryCurrency,
        //                JobDescription = dto.Description,
        //                ExperienceLevel = dto.ExperienceLevel,
        //                ApplicationEmail = dto.ContactEmail,
        //                DeadLine = dto.ApplicationDeadline,
        //                Created = DateTime.UtcNow
        //            };

        //            _context.JobDetails.Add(job);
        //            await _context.SaveChangesAsync();
        //        }


        //        // Insert new Benefits
        //        if (dto.Benefits != null && dto.Benefits.Count > 0)
        //        {
        //            foreach (var benefitId in dto.Benefits)
        //            {
        //                _context.JobBenefits.Add(new JobBenefits
        //                {
        //                    jobId = job.JobId,
        //                    BenefitId = benefitId
        //                });
        //            }
        //        }

        //        // Insert new Skills
        //        if (dto.Skills != null && dto.Skills.Count > 0)
        //        {
        //            foreach (var skillId in dto.Skills)
        //            {
        //                _context.JobSkills.Add(new JobSkills
        //                {
        //                    JobId = job.JobId,
        //                    skillId = skillId
        //                });
        //            }
        //        }

        //     int result=   await _context.SaveChangesAsync();
        //        await transaction.CommitAsync();
        //        return result;


        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        return 0;
        //     //   return BadRequest(new { Success = false, Message = ex.Message });
        //    }

        //}
        public async Task<int> SaveJob(JobCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                JobDetails job;

                if (dto.JobId == 0)
                {
                    // 🆕 CREATE new job
                    job = new JobDetails
                    {
                        JobTitle = dto.Title,
                        CompanyID = dto.Company,
                        DepartmentId = dto.Department,
                        Address = dto.Location,
                        jobTypeId = dto.Type,
                        JobLink = dto.ApplicationLink,
                        NoOfOpenings = dto.Vacancies,
                        MinimumSalary = dto.SalaryMin.ToString(),
                        MaximumSalary = dto.SalaryMax.ToString(),
                        Currency = dto.SalaryCurrency,
                        JobDescription = dto.Description,
                        ExperienceLevel = dto.ExperienceLevel,
                        ApplicationEmail = dto.ContactEmail,
                        DeadLine = dto.ApplicationDeadline,
                        Created = DateTime.UtcNow
                    };

                    _context.JobDetails.Add(job);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // ✏️ UPDATE existing job
                    job = await _context.JobDetails.FirstOrDefaultAsync(x => x.JobId == dto.JobId);

                    if (job == null)
                        throw new Exception("Job not found");

                    job.JobTitle = dto.Title;
                    job.CompanyID = dto.Company;
                    job.DepartmentId = dto.Department;
                    job.Address = dto.Location;
                    job.jobTypeId = dto.Type;
                    job.JobLink = dto.ApplicationLink;
                    job.NoOfOpenings = dto.Vacancies;
                    job.MinimumSalary = dto.SalaryMin.ToString();
                    job.MaximumSalary = dto.SalaryMax.ToString();
                    job.Currency = dto.SalaryCurrency;
                    job.JobDescription = dto.Description;
                    job.ExperienceLevel = dto.ExperienceLevel;
                    job.ApplicationEmail = dto.ContactEmail;
                    job.DeadLine = dto.ApplicationDeadline;
                    job.Updateddate = DateTime.UtcNow;

                    _context.JobDetails.Update(job);

                    // 🔄 Remove existing benefits & skills (to re-insert)
                    var existingBenefits = _context.JobBenefits.Where(b => b.jobId == job.JobId);
                    _context.JobBenefits.RemoveRange(existingBenefits);

                    var existingSkills = _context.JobSkills.Where(s => s.JobId == job.JobId);
                    _context.JobSkills.RemoveRange(existingSkills);
                }

                // ✅ Re-insert Benefits
                if (dto.Benefits != null && dto.Benefits.Count > 0)
                {
                    foreach (var benefitId in dto.Benefits)
                    {
                        _context.JobBenefits.Add(new JobBenefits
                        {
                            jobId = job.JobId,
                            BenefitId = benefitId
                        });
                    }
                }

                // ✅ Re-insert Skills
                if (dto.Skills != null && dto.Skills.Count > 0)
                {
                    foreach (var skillId in dto.Skills)
                    {
                        _context.JobSkills.Add(new JobSkills
                        {
                            JobId = job.JobId,
                            skillId = skillId
                        });
                    }
                }

                int result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Optionally log ex.Message here
                return 0;
            }
        }

        public async Task<int> ApplyForJob(ApplyForJob dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                JobApplicationCandidate jb = new JobApplicationCandidate();
                jb.JobId = dto.JobId;
                jb.CandidateId = dto.CandidateId;
                jb.AppliedDate = DateTime.UtcNow;
                jb.Status = dto.Status;
                jb.CreatedBy = dto.CreatedBy;


                _context.JobApplicationCandidate.Add(jb);
                int result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Optionally log ex.Message here
                return 0;
            }
        }

        public async Task<JobCreateDto> GetJobById(int jobId)
        {
            try
            {
                var job = await (from j in _context.JobDetails
                                 where j.JobId == jobId
                                 select new JobCreateDto
                                 {
                                     Title = j.JobTitle,
                                     Company = (int)j.CompanyID,
                                     Department = (int)j.DepartmentId,
                                     Location = j.Address,
                                     Type = (int)j.jobTypeId,
                                     Vacancies = (int)j.NoOfOpenings,
                                     SalaryMin = Convert.ToDecimal(j.MinimumSalary),
                                     SalaryMax = Convert.ToDecimal(j.MaximumSalary),
                                     SalaryCurrency = j.Currency,
                                     Description = j.JobDescription,
                                     ExperienceLevel = j.ExperienceLevel,
                                     ContactEmail = j.ApplicationEmail,
                                     ApplicationDeadline = (DateTime)j.DeadLine,
                                     ApplicationLink=j.JobLink,
                                     // Fetch related Benefits
                                     Benefits = (from jb in _context.JobBenefits
                                                 where jb.jobId == j.JobId
                                                 select jb.BenefitId).ToList(),

                                     // Fetch related Skills
                                     Skills = (from js in _context.JobSkills
                                               where js.JobId == j.JobId
                                               select js.skillId).ToList()
                                 }).FirstOrDefaultAsync();

                return job;
            }
            catch (Exception ex)
            {
                // Log or handle exception here if needed
                throw new Exception("Error while fetching job details: " + ex.Message, ex);
            }
        }

        public async Task<List<JobListDto>> GetJobs(int hrid)
        {
            var jobs = (from j in _context.JobDetails
                        join
                       dept in _context.DepartmentMaster on j.DepartmentId equals dept.DepartmentId
                        join c in _context.Company on j.CompanyID equals c.CompanyId
                        join hc in _context.HRCompany on c.CompanyId equals hc.CompanyId
                        join jt in _context.JobType on j.jobTypeId equals jt.Id
                        where hc.HRId == hrid


                        select new JobListDto
                        {
                            Id = j.JobId,
                            Title = j.JobTitle,
                            Department = dept.DepartmentName, // Map via DepartmentId later
                            Location = j.Address,
                            Description = j.JobDescription,
                            Salary = JobRepository.FormatSalary(j.MinimumSalary, j.MaximumSalary, j.Currency),// j.MinimumSalary + " - " + j.MaximumSalary,
                            Experience = j.ExperienceLevel,
                            Type = jt.Name,
                            Skills = (from js in _context.JobSkills
                                      join sm in _context.SkillMaster on js.skillId equals sm.SkillID
                                      where js.JobId == j.JobId
                                      select sm.SkillName                                    ).ToList().ToArray(),
                            ApplicantCount = 0,
                            NewApplicants = 0,
                            Interviews = 0,
                            PostedDate = j.Created ?? DateTime.Now,
                            Status = "Active"
                        }).ToList().DistinctBy(x=>x).ToList();
                //.ToListAsync();

            return jobs;
        }

        public async Task<List<JobListForEmployee>> ViewJobs(int EmpId)
        {
            var jobs = (from j in _context.JobDetails
                        join
                       dept in _context.DepartmentMaster on j.DepartmentId equals dept.DepartmentId
                        join c in _context.Company on j.CompanyID equals c.CompanyId
                        join hc in _context.HRCompany on c.CompanyId equals hc.CompanyId
                        join jt in _context.JobType on j.jobTypeId equals jt.Id
                        


                        select new JobListForEmployee
                        {
                            Id = j.JobId,
                            Title = j.JobTitle,
                            Department = dept.DepartmentName, // Map via DepartmentId later
                            Location = j.Address,
                            Description = j.JobDescription,
                            Salary = JobRepository.FormatSalary(j.MinimumSalary, j.MaximumSalary, j.Currency),// j.MinimumSalary + " - " + j.MaximumSalary,
                            Experience = j.ExperienceLevel,
                            Type = jt.Name,
                            ApplicantCount = 0,
                            NewApplicants = 0,
                            Interviews = 0,
                            PostedDate = j.Created ?? DateTime.Now,
                            Status = "Active",
                            employeeStatusForJob= _context.JobApplicationCandidate.Where(x=>x.CandidateId==EmpId && x.JobId==j.JobId).FirstOrDefault()==null?"0": _context.JobApplicationCandidate.Where(x => x.CandidateId == EmpId && x.JobId == j.JobId).FirstOrDefault().Status
                        }).ToList().DistinctBy(x => x.Id).ToList();
            //.ToListAsync();

            return jobs;
        }
        // ✅ Helper: Format Salary based on currency
        public static string FormatSalary(string? min, string? max, string? currency)
        {
            if (string.IsNullOrEmpty(min) || string.IsNullOrEmpty(max))
                return "Not specified";

            if (!decimal.TryParse(min, out decimal minVal) || !decimal.TryParse(max, out decimal maxVal))
                return $"{min} - {max}";

            string symbol = currency?.ToUpper() switch
            {
                "USD" => "$",
                "EUR" => "€",
                "GBP" => "£",
                "CAD" => "CA$",
                "AUD" => "A$",
                "RUPEE" or "INR" => "₹",
                _ => ""
            };

            bool isIndian = currency?.ToUpper() is "INR" or "RUPEE";

            string formattedMin = JobRepository.FormatAmount(minVal, isIndian);
            string formattedMax = JobRepository.FormatAmount(maxVal, isIndian);

            return $"{symbol}{formattedMin} - {symbol}{formattedMax}";
        }
        private static string FormatAmount(decimal amount, bool isIndian)
        {
            if (isIndian)
            {
                // Indian numbering system (1,23,456)
                return string.Format(new System.Globalization.CultureInfo("en-IN"), "{0:N0}", amount);
            }
            else
            {
                // Western numbering system (1,234,567)
                return string.Format("{0:N0}", amount);
            }
        }

    }
}
