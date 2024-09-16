using Core.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var employees = new List<Employee>();

app.MapGet("/employees", () => employees).WithOpenApi();

app.MapGet("/employees/{id}", (int id) =>
{
    var employee = employees.FirstOrDefault(e => e.Id == id);
    return employee is not null ? Results.Ok(employee) : Results.NotFound();
}).WithOpenApi();

app.MapPost("/employees", (Employee employee) =>
{
    employees.Add(employee);
    return Results.Created($"/employees/{employee.Id}", employee);
}).WithOpenApi();

app.MapPut("/employees/{id}", (int id, Employee updatedEmployee) =>
{
    var employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee is null) return Results.NotFound();
    employee.FirstName = updatedEmployee.FirstName;
    employee.LastName = updatedEmployee.LastName;
    employee.Mobile = updatedEmployee.Mobile;
    employee.Email = updatedEmployee.Email;
    employee.Address = updatedEmployee.Address;
    return Results.NoContent();
}).WithOpenApi();

app.MapDelete("/employees/{id}", (int id) =>
{
    var employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee is null) return Results.NotFound();
    employees.Remove(employee);
    return Results.NoContent();
}).WithOpenApi();
app.Run();


