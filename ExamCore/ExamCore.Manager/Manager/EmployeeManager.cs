using ExamCore.Manager.Base;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Models;
using ExamCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Manager.Manager
{
    public class EmployeeManager : BaseManager<Employee>, IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository): base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ICollection<Employee>?> GetAllEmloyeeAsync()
        {
           var employees = await _employeeRepository.GetAllEmployeeAsync();
            if(employees != null)
            {
                return employees;
            }
            else
            {
                return null;
            }
        }

        public async Task<Employee?> GetEmloyeeById(int employeeId)
        {
            var employee = await _employeeRepository.GetEmloyeeById(employeeId);
            if (employee != null)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
