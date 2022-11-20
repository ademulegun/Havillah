using Havillah.ApplicationServices.Extensions;
using Havillah.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("");
builder.Services.AddDbContext<DatabaseContext>(x=>x.UseSqlServer(connectionString));

builder.Services.AddControllers();
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

builder.Services.AddMediatR();

#region items
app.MapPost("/item", () =>
{

}).WithName("AddNewItem").WithTags("Item");

app.MapGet("/items", () =>
{
   
}).WithName("GetAllItems").WithTags("Item");

app.MapPost("/items/{name}", (string name) =>
{

}).WithName("GetItemByName").WithTags("Item");
#endregion
app.Run();