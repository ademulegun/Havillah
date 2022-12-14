using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Havillah.ApplicationServices.Authentication.Dto;
using Havillah.ApplicationServices.Authentication.UseCases;
using Havillah.ApplicationServices.Extensions;
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.AddProduct.Handlers;
using Havillah.ApplicationServices.Product.UseCases.AddProduct.Dto;
using Havillah.Core.Domain;
using Havillah.Persistence;
using Havillah.Persistense.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HavillahConnection");
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(option =>
{
    option.Tokens.EmailConfirmationTokenProvider = "emailConfirmation";
}).AddEntityFrameworkStores<DatabaseContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
});
//Configure DI
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("V1", new OpenApiInfo
//     {
//         Version = "V1",
//         Title = "HIMS",
//         Description = "Inventory Management WebAPI"
//     });
//     options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Scheme = "Bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Name = "Authorization",
//         Description = "Bearer Authentication with JWT Token",
//         Type = SecuritySchemeType.Http
//     });
//     options.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Id = "Bearer",
//                     Type = ReferenceType.SecurityScheme
//                 }
//             },
//             new List<string>()
//         }
//     });
// });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#region Authentication
app.MapPost("/security/generateToken",  [AllowAnonymous]async(LoginDto user, IMediator mediator) =>
{
    var token = await mediator.Send(new ValidateUserCommand()
    {
        Email = user.Email, Password = user.Password, RememberMe = user.RememberMe
    });
    return string.IsNullOrEmpty(token.Value) ? Results.Unauthorized() : Results.Ok(token.Value);
}).WithName("generateToken").WithTags("Authentication");

app.MapPost("/registerUser", async ([FromBody] AddProductDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand() { AddProductDto = model });
    return Results.Ok();
}).WithName("RegisterUser").WithTags("Authentication");

#endregion

#region Product

app.MapPost("/product", async ([FromBody] AddProductDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand() { AddProductDto = model });
    return Results.Ok();
}).WithName("AddProduct").WithTags("Product");

#endregion

app.Run();