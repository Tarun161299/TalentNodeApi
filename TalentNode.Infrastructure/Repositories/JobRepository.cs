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
        public async Task<int> SaveJob( JobCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                JobDetails job=new JobDetails();
                bool isUpdate = false;// dto.job.HasValue;

                if (!isUpdate)
                {
                    // Create new job
                    job = new JobDetails
                    {
                        JobTitle = dto.Title,
                        CompanyID = dto.Company,
                        DepartmentId = dto.Department,
                        Address = dto.Location,
                        jobTypeId = dto.Type,
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
                

                // Insert new Benefits
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

                // Insert new Skills
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

             int result=   await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return 0;
             //   return BadRequest(new { Success = false, Message = ex.Message });
            }

        }
    }
}
