using AutoMapper;
using ExamCore.Application.ApplicationLogic.CountryLogic.Command;
using ExamCore.Application.ApplicationLogic.CountryLogic.Queries;
using ExamCore.Database.DatabaseContexts;
using ExamCore.Manager.Contracts;
using ExamCore.Model.Dto;
using ExamCore.Model.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExamCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            if (employees == null || !employees.Any())
            {
                return NotFound(new { Message = "No Employee found." });
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var employee = await _mediator.Send(query);
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

                var command = new AddEmployeeCommand { EmployeeDto = employeeDto };
                var employee = await _mediator.Send(command);

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

                var command = new UpdateEmployeeCommand { Id = id, EmployeeDto = employeeDto };
                var updatedEmployee = await _mediator.Send(command);

                return Ok(new { Message = "Employee updated successfully.", Employee = updatedEmployee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while updating the employee.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteEmployee(int id)
        {
            try
            {
                var command = new SoftDeleteEmployeeCommand { Id = id };
                var success = await _mediator.Send(command);

                if (!success)
                    return NotFound(new { Message = "Employee not found." });

                return Ok(new { Message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while deleting the employee.", Error = ex.Message });
            }
        }
    }

}
