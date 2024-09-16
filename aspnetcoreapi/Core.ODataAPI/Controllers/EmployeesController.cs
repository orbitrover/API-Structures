using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Core.ODataAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static List<Employee> Employees = new List<Employee>();

        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            return Employees.AsQueryable();
        }
    }
}
