using AutoMapper;
using ExamCore.Database.DatabaseContexts;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Dto;
using ExamCore.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly ICountryManager _countryManager;
        private readonly IMapper _mapper;
        public EmployeeController(IMapper mapper, IEmployeeManager employeeManager, ICountryManager countryManager)
        {
            _mapper = mapper;
            _employeeManager = employeeManager;
            _countryManager = countryManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await _employeeManager.GetAllEmloyeeAsync();
            if (employees == null) { 
                return NotFound(new { Message = "No Employee found." });
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeManager.GetEmloyeeById(id);
            if (employee == null)
                return NotFound(new { Message = "Employee not found." });

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existCountry = await _countryManager.GetByIdAsync(employeeDto.CountryId);
                if (existCountry == null)
                    return BadRequest(new { Message = "Country not found." });

                var employee = _mapper.Map<Employee>(employeeDto);
                employee.CreatedDateTime = DateTime.UtcNow;
                employee.CreatedById = "System";

                await _employeeManager.CreateAsync(employee);

                return Ok(new { Message = "Employee added successfully.", Employee = employee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while creating the employee.", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingEmployee = await _employeeManager.GetByIdAsync(id);
                if (existingEmployee == null)
                    return NotFound(new { Message = "Employee not found." });

                var existCountry = await _countryManager.GetByIdAsync(employeeDto.CountryId);
                if (existCountry == null)
                    return BadRequest(new { Message = "Country not found." });

                _mapper.Map(employeeDto, existingEmployee);
                existingEmployee.UpdatedDateTime = DateTime.UtcNow;
                existingEmployee.UpdatedById = "System";

                await _employeeManager.UpdateAsync(existingEmployee);

                return Ok(new { Message = "Employee updated successfully.", Employee = existingEmployee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while updating the employee.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteEMaployee(int id)
        {
            try
            {
                var existingEmployee = await _employeeManager.GetEmloyeeById(id);
                if (existingEmployee == null)
                    return NotFound(new { Message = "Employee not found." });

                existingEmployee.IsDeleted = true;
                existingEmployee.DeletedDateTime = DateTime.UtcNow;

                await _employeeManager.UpdateAsync(existingEmployee);

                return Ok(new { Message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while deleting the employee.", Error = ex.Message });
            }
        }
    }
}
