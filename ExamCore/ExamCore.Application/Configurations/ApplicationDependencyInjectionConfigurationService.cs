using System.Reflection;
using ExamCore.Manager.Contracts;
using ExamCore.Manager.Manager;
using ExamCore.Repository.Contracts;
using ExamCore.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExamCore.Application.Configurations
{
    public static class ApplicationDependencyInjectionConfigurationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // Add automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutomapperMappingProfile>();
            }, Assembly.GetExecutingAssembly());

            // Add mediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // Register IHttpContextAccessor 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Add Manager and Repository Services
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            
            #endregion

            return services;
        }
    }
}