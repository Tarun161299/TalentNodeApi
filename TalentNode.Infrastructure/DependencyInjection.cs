using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return services;
        }
    }
}
