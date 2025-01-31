using System.Reflection;
using ExamCore.Application.ApplicationLogic.CountryLogic.Command;
using ExamCore.Application.ApplicationLogic.CountryLogic.Queries;
using ExamCore.Manager.Contracts;
using ExamCore.Manager.Manager;
using ExamCore.Model.Dto;
using ExamCore.Model.Models;
using ExamCore.Repository.Base;
using ExamCore.Repository.Contracts;
using ExamCore.Repository.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static ExamCore.Application.ApplicationLogic.CountryLogic.Command.AddEmployeeCommand;
using static ExamCore.Application.ApplicationLogic.CountryLogic.Command.SoftDeleteEmployeeCommand;
using static ExamCore.Application.ApplicationLogic.CountryLogic.Command.UpdateEmployeeCommand;
using static ExamCore.Application.ApplicationLogic.CountryLogic.Queries.GetAllEmployeesQuery;
using static ExamCore.Application.ApplicationLogic.CountryLogic.Queries.GetEmployeeByIdQuery;

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
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
            services.AddTransient<IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>, GetAllEmployeesQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>, GetEmployeeByIdQueryHandler>();
            services.AddTransient<IRequestHandler<AddEmployeeCommand, EmployeeDto>, AddEmployeeCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateEmployeeCommand, EmployeeDto>, UpdateEmployeeCommandHandler>();
            services.AddTransient<IRequestHandler<SoftDeleteEmployeeCommand, bool>, SoftDeleteEmployeeCommandHandler>();

            #endregion

            return services;
        }
    }
}