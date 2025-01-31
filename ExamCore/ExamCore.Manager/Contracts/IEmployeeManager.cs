using ExamCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Manager.Contracts
{
    public interface IEmployeeManager : IBaseManager<Employee>
    {
        Task<ICollection<Employee>?> GetAllEmloyeeAsync();
        Task<Employee?> GetEmloyeeById(int employeeId);
    }
}
