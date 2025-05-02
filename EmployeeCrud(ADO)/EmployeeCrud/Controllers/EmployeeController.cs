using EmployeeCrud.Models;
using EmployeeCrud.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        // POST: api/student
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            _employeeService.AddEmployee(employee);
            return Ok(new { message = "Employee added successfully" });
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            employee.Id= id;
            _employeeService.UpdateEmployee(employee);
            return Ok(new { message = "Employee updated successfully" });
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok(new { message = "Emplo deleted successfully" });
        }
        [HttpGet("{id}")]
            public IActionResult GetEmployeeById(int id)
        {
            _employeeService.GetEmployeeById(id);
            return Ok( "Employee Updated successfully" );
        }

    }
}
