using ExamCore.Model.Models;
using ExamCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Repository.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<ICollection<Employee>> GetAllEmployeeAsync();
        Task<Employee?> GetEmloyeeById(int employeeId);
    }
}
