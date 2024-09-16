using Core.Model;

namespace Core.GraphQL
{
    public class Query
    {
        private List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Mobile = "1234567890", Email = "john@example.com", Address = "123 Main St" }
        };

        public Employee GetEmployee(int id) => employees.FirstOrDefault(e => e.Id == id);

        public List<Employee> GetAllEmployees() => employees;
    }
}
