using Core.gRPCService;
using Core.Model;
using Grpc.Core;

namespace Core.gRPCService.Services
{
    public class EmployeeService : EmployeeService.EmployeeServiceBase
    {
        private static List<Employee> employees = new List<Employee>();

        public override Task<EmployeeResponse> GetEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var employee = employees.FirstOrDefault(e => e.Id == request.Id);
            if (employee is null) throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));
            return Task.FromResult(new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mobile = employee.Mobile,
                Email = employee.Email,
                Address = employee.Address
            });
        }

        public override Task<EmployeeListResponse> GetAllEmployees(EmptyRequest request, ServerCallContext context)
        {
            var response = new EmployeeListResponse();
            response.Employees.AddRange(employees.Select(e => new Employee
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Mobile = e.Mobile,
                Email = e.Email,
                Address = e.Address
            }));
            return Task.FromResult(response);
        }

        public override Task<EmptyResponse> AddEmployee(Employee request, ServerCallContext context)
        {
            employees.Add(new Employee
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mobile = request.Mobile,
                Email = request.Email,
                Address = request.Address
            });
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<EmptyResponse> UpdateEmployee(Employee request, ServerCallContext context)
        {
            var employee = employees.FirstOrDefault(e => e.Id == request.Id);
            if (employee is null) throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Mobile = request.Mobile;
            employee.Email = request.Email;
            employee.Address = request.Address;
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<EmptyResponse> DeleteEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var employee = employees.FirstOrDefault(e => e.Id == request.Id);
            if (employee is null) throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));
            employees.Remove(employee);
            return Task.FromResult(new EmptyResponse());
        }
    }
}
