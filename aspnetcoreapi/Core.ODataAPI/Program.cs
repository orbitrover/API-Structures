using Core.Model;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Buffers;
using Microsoft.OData.Evaluation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().Expand().OrderBy().SetMaxTop(100).Count());
// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapODataRoute("odata", "odata", GetEdmModel());
    endpoints.MapControllers();
});
app.Run();
IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Employee>("Employees");
    return builder.GetEdmModel();
}