using Microsoft.EntityFrameworkCore;
using TalentNode.Domain.Entities;


namespace TalentNode.Infrastructure.Data
{
    public class TalentNodeDbContext(DbContextOptions<TalentNodeDbContext> Options):DbContext(Options)
    {
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public DbSet<EmployeEntity> SignupDetails { get; set; }
        public DbSet<DistrictMaster> DistrictMaster { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkill { get; set; } 

        public DbSet<QualificationMaster> QualificationMaster { get; set; }
        public DbSet<SkillMaster> SkillMaster { get; set; }

        public DbSet<StateMaster> StateMaster { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<MDModule> MdModule { get; set; }
        public DbSet<MdMainModule> MdMainModule { get; set; }
        public DbSet<MdRoleModule> MdRoleModule { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<EmployeeQualification> EmployeeQualification { get; set; }
        public DbSet<EmployeeExperiences> EmployeeExperiences { get; set; }
        public DbSet<DepartmentMaster> DepartmentMaster { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<HRdetails> HRdetails { get; set; }

        public DbSet<HRCompany> HRCompany { get; set; }
        public DbSet<JobDetails> JobDetails { get; set; }
        public DbSet<JobBenefits> JobBenefits { get; set; }
        public DbSet<JobSkills> JobSkills { get; set; }
        public DbSet<BenefitMaster> BenefitMaster { get; set; }
        public DbSet<JobType> JobType { get; set; }

        public DbSet<CandidateStatusMaster> CandidateStatusMaster { get; set; }

        public DbSet<JobApplicationCandidate> JobApplicationCandidate { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MdMainModule>()
     .HasKey(m => new { m.Roleid, m.MainModuleID });
            modelBuilder.Entity<MdRoleModule>()
  .HasKey(m => new { m.MainModuleID, m.ModuleID });
            modelBuilder.Entity<JobApplicationCandidate>()
        .HasKey(c => new { c.CandidateId, c.JobId });

            // Composite keys
            modelBuilder.Entity<EmployeeQualification>()
                .HasKey(eq => new { eq.EmpID, eq.QualID });
            modelBuilder.Entity<JobBenefits>()
                .HasKey(eq => new { eq.jobId, eq.BenefitId });
            modelBuilder.Entity<JobSkills>()
               .HasKey(eq => new { eq.JobId, eq.skillId });
            modelBuilder.Entity<HRCompany>()
                .HasKey(eq => new { eq.HRId, eq.CompanyId });
            modelBuilder.Entity<UserRoleMapping>()
            .HasKey(ur => new { ur.UserName, ur.RoleId });

            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(es => new { es.EmployeeID, es.SkillID });

            // Disable cascade delete between Employee → StateMaster
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateID)
                .OnDelete(DeleteBehavior.Restrict);

            // Disable cascade delete between Employee → DistrictMaster
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.District)
                .WithMany()
                .HasForeignKey(e => e.DistrictID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeExperiences>()
            .HasKey(e => new { e.EmployeeID, e.ExperienceID });
        }

    }
}
