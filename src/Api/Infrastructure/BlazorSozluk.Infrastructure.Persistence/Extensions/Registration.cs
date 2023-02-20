using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration connectionString) 
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var connStr = connectionString["BlazorSozlukDbConnectionString"].ToString();
                conf.UseSqlServer("",opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            var seedData = new SeedData();

            seedData.SeedAsync(connectionString).GetAwaiter().GetResult();


            return services;
        }
    }
}
