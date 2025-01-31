using ExamCore.Manager.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Application.ApplicationLogic.CountryLogic.Command
{
    public class SoftDeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class SoftDeleteEmployeeCommandHandler : IRequestHandler<SoftDeleteEmployeeCommand, bool>
        {
            private readonly IEmployeeManager _employeeManager;

            public SoftDeleteEmployeeCommandHandler(IEmployeeManager employeeManager)
            {
                _employeeManager = employeeManager;
            }

            public async Task<bool> Handle(SoftDeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var existingEmployee = await _employeeManager.GetEmloyeeById(request.Id);
                if (existingEmployee == null)
                    throw new Exception("Employee not found.");

                existingEmployee.IsDeleted = true;
                existingEmployee.DeletedDateTime = DateTime.UtcNow;

                await _employeeManager.UpdateAsync(existingEmployee);
                return true;
            }
        }

    }

}
