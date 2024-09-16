using Core.Model;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Core.AzureFunctionsServerlessAPI
{
    public static class EmployeeFunction
    {
        private static List<Employee> employees = new List<Employee>();

        [FunctionName("GetEmployee")]
        public static IActionResult GetEmployee([HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees/{id}")] HttpRequest req, int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee is not null ? new OkObjectResult(employee) : new NotFoundResult();
        }

        [FunctionName("AddEmployee")]
        public static IActionResult AddEmployee([HttpTrigger(AuthorizationLevel.Function, "post", Route = "employees")] Employee employee)
        {
            employees.Add(employee);
            return new OkResult();
        }
    }
}
