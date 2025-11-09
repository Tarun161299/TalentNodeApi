using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalentNode.Domain.interfaces;
using TalentNode.Infrastructure.Data;
using TalentNode.Infrastructure.Repositories;

namespace TalentNode.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastuctureDI(this IServiceCollection services)
        {
            services.AddDbContext<TalentNodeDbContext>(options => { options.UseSqlServer("Server =SHIV\\SQLEXPRESS_2022;Database=TalentNode;Trusted_Connection=true; Encrypt=true; TrustServerCertificate=true;"); });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
            services.AddScoped<IMdModuleRepository, ModulesByRoleRepository>();
            services.AddScoped<SignupInterface, SignupRepository>();
            services.AddScoped<UserProfileInterface, UserProfileRepository>();
            services.AddScoped<IMdRepository, MdRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            return services;
        }
    }
}
