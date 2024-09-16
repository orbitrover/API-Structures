using Microsoft.AspNetCore.SignalR;
using Core.Model;

namespace Core.SignalRAPI.Hubs
{
    public class EmployeeHub : Hub
    {
        public async Task AddEmployee(Employee employee)
        {
            await Clients.All.SendAsync("ReceiveEmployee", employee);
        }
    }

}
