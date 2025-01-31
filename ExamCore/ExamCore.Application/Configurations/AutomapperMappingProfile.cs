using System.Reflection;
using AutoMapper;
using ExamCore.Model.Dto;
using ExamCore.Model.Models;
using ExamCore.Shared.Mappings;

namespace ExamCore.Application.Configurations
{
    public class AutomapperMappingProfile : Profile
    {
        public AutomapperMappingProfile()
        {
            // Add the mappings from all types that implement IMapFrom<T> interface
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Employee, EmployeeDto>()
            .ForMember(c => c.CountryName, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<EmployeeDto, Employee>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}