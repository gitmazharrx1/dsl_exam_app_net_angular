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
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
        {
            private readonly IEmployeeManager _employeeManager;
            private readonly IMapper _mapper;

            public GetEmployeeByIdQueryHandler(IEmployeeManager employeeManager, IMapper mapper)
            {
                _employeeManager = employeeManager;
                _mapper = mapper;
            }

            public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeManager.GetEmloyeeById(request.Id);
                return _mapper.Map<EmployeeDto>(employee);
            }
        }

    }

}
