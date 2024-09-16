using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Core.RESTfulAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> Employees = new List<Employee>();

        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            return Employees;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            return employee;
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            Employees.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Employee updatedEmployee)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Mobile = updatedEmployee.Mobile;
            employee.Email = updatedEmployee.Email;
            employee.Address = updatedEmployee.Address;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            Employees.Remove(employee);
            return NoContent();
        }
    }
}
