using AutoMapper;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Application.ApplicationLogic.CountryLogic.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeDto>> {
        public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
        {
            private readonly IEmployeeManager _employeeManager;
            private readonly IMapper _mapper;

            public GetAllEmployeesQueryHandler(IEmployeeManager employeeManager, IMapper mapper)
            {
                _employeeManager = employeeManager;
                _mapper = mapper;
            }

            public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var employees = await _employeeManager.GetAllEmloyeeAsync();
                return _mapper.Map<List<EmployeeDto>>(employees);
            }
        }

    }

}
