using AutoMapper;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Application.ApplicationLogic.CountryLogic.Command
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
        {
            private readonly IEmployeeManager _employeeManager;
            private readonly ICountryManager _countryManager;
            private readonly IMapper _mapper;

            public UpdateEmployeeCommandHandler(IEmployeeManager employeeManager, ICountryManager countryManager, IMapper mapper)
            {
                _employeeManager = employeeManager;
                _countryManager = countryManager;
                _mapper = mapper;
            }

            public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var existingEmployee = await _employeeManager.GetByIdAsync(request.Id);
                if (existingEmployee == null)
                    throw new Exception("Employee not found.");

                var existCountry = await _countryManager.GetByIdAsync(request.EmployeeDto.CountryId);
                if (existCountry == null)
                    throw new Exception("Country not found.");

                _mapper.Map(request.EmployeeDto, existingEmployee);
                existingEmployee.UpdatedDateTime = DateTime.UtcNow;
                existingEmployee.UpdatedById = "System";

                await _employeeManager.UpdateAsync(existingEmployee);
                return _mapper.Map<EmployeeDto>(existingEmployee);
            }
        }

    }

}
