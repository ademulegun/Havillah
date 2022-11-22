using Havillah.ApplicationServices.Extensions;
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.AddProduct.Dto;
using Havillah.ApplicationServices.Product.AddProduct.Handlers;
using Havillah.Persistence;
using Havillah.Persistense.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HavillahConnection");
builder.Services.AddDbContext<DatabaseContext>(x=>x.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR();

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
app.MapPost("/product", async([FromBody]AddProductDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand(){AddProductDto = model});
    return Results.Ok();
}).WithName("AddProduct").WithTags("Product");

#endregion
app.Run();