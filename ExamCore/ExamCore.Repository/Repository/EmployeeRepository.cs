using ExamCore.Model.Models;
using ExamCore.Repository.Base;
using ExamCore.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Repository.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>,IEmployeeRepository
    {

        public async Task<ICollection<Employee>> GetAllEmployeeAsync()
        {
            return await dbContext.Employees.Where(e => !e.IsDeleted).Include(e => e.Country).ToListAsync();
        }
        public async Task<Employee?> GetEmloyeeById(int employeeId)
        {
            return await dbContext.Employees.Where(e => !e.IsDeleted && e.Id == employeeId).Include(e => e.Country).FirstOrDefaultAsync();
            
        }
    }
}
