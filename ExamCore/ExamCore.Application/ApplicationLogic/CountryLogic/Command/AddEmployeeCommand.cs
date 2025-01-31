using AutoMapper;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Dto;
using ExamCore.Model.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Application.ApplicationLogic.CountryLogic.Command
{
    public class AddEmployeeCommand : IRequest<EmployeeDto>
    {
        public EmployeeDto EmployeeDto { get; set; }
        public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeDto>
        {
            private readonly IEmployeeManager _employeeManager;
            private readonly ICountryManager _countryManager;
            private readonly IMapper _mapper;

            public AddEmployeeCommandHandler(IEmployeeManager employeeManager, ICountryManager countryManager, IMapper mapper)
            {
                _employeeManager = employeeManager;
                _countryManager = countryManager;
                _mapper = mapper;
            }

            public async Task<EmployeeDto> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
            {
                var existCountry = await _countryManager.GetByIdAsync(request.EmployeeDto.CountryId);
                if (existCountry == null)
                    throw new Exception("Country not found.");

                var employee = _mapper.Map<Employee>(request.EmployeeDto);
                employee.CreatedDateTime = DateTime.UtcNow;
                employee.CreatedById = "System";

                await _employeeManager.CreateAsync(employee);
                return _mapper.Map<EmployeeDto>(employee);
            }
        }

    }

}
