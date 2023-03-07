using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Havillah.ApplicationServices.Authentication.Dto;
using Havillah.ApplicationServices.Authentication.UseCases.Commands;
using Havillah.ApplicationServices.Authentication.UseCases.Queries;
using Havillah.ApplicationServices.Category.UseCases.AddCategory.Handler;
using Havillah.ApplicationServices.Category.UseCases.Dto;
using Havillah.ApplicationServices.Category.UseCases.GetCategories.Handler;
using Havillah.ApplicationServices.Common.Options;
using Havillah.ApplicationServices.Expense.Dto;
using Havillah.ApplicationServices.Expense.UseCases.Commands;
using Havillah.ApplicationServices.Expense.UseCases.Queries;
using Havillah.ApplicationServices.Extensions;
using Havillah.ApplicationServices.Interfaces;
using Havillah.ApplicationServices.Product.AddProduct.Handlers;
using Havillah.ApplicationServices.Product.UseCases.AddProduct.Dto;
using Havillah.ApplicationServices.Product.UseCases.GetProduct.Dto;
using Havillah.ApplicationServices.Product.UseCases.GetProduct.Handlers;
using Havillah.ApplicationServices.Product.UseCases.GetProducts.Handlers;
using Havillah.ApplicationServices.Product.UseCases.UpdateProduct.Handlers;
using Havillah.ApplicationServices.User.Dto;
using Havillah.ApplicationServices.User.UseCases.Commands;
using Havillah.ApplicationServices.User.UseCases.Queries;
using Havillah.Core.Domain;
using Havillah.Infrasructure.Storage;
using Havillah.Persistence;
using Havillah.Persistense.Repository;
using Havillah.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using LoginDto = Havillah.ApplicationServices.Authentication.Dto.LoginDto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HavillahConnection");

if (!builder.Environment.IsDevelopment())
{
    var azureAppConfigConnectionString = "Endpoint=https://himsbackendappconfig.azconfig.io;Id=DU2q-ly-s0:zxCuonWONKo+FDi3xCHl;Secret=dTbXlp2lA6c/gnfJvgXIghAv3hR+TxGfOGKX0sSMhFk=";
    // Load configuration from Azure App Configuration
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(azureAppConfigConnectionString).ConfigureRefresh((refreshOptions) =>
        {
            // indicates that all configuration should be refreshed when the given key has changed.
            refreshOptions.Register(key: "sentinel", refreshAll: true);
            refreshOptions.SetCacheExpiration(TimeSpan.FromSeconds(5));
        }).UseFeatureFlags();
    });
}
var val = builder.Configuration;
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentityCore<ApplicationUser>(option =>
{
    option.Tokens.EmailConfirmationTokenProvider = "emailConfirmation";
}).AddRoles<IdentityRole<Guid>>().AddEntityFrameworkStores<DatabaseContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
});
//Configure DI
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUploadImageToStorage, UploadImage>();
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
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

if (!app.Environment.IsDevelopment())
{
    app.UseAzureAppConfiguration();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#region Authentication

app.MapPost("/security/generateToken",  [AllowAnonymous]async(LoginDto user, IMediator mediator) =>
{
    var token = await mediator.Send(new ValidateUserUseCaseCommand()
    {
        Email = user.Email, Password = user.Password, RememberMe = user.RememberMe
    });
    if (string.IsNullOrEmpty(token.Value.TokenValue))
    {
        Results.Unauthorized();
    }
    token.ResponseCode = "00";
    return Results.Ok(token);
}).WithName("Login").WithTags("Authentication")
    .Produces<Token>(StatusCodes.Status200OK)
    .Produces<Token>(StatusCodes.Status400BadRequest);

app.MapPost("/registerUser", async ([FromBody] RegisterUserDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new RegisterUserUseCaseCommand()
    {
        UserName = model.UserName, Email = model.Email,
        Password = model.Password, FirstName = model.FirstName,
        MiddleName = model.MiddleName, LastName = model.LastName,
        Address = model.Address, PhoneNumber = model.PhoneNumber
    });
    return !result.IsSuccess ? Results.BadRequest(result.Value) : Results.Ok(result.Value);
}).WithName("RegisterUser").WithTags("Authentication")
    .Produces<string>(StatusCodes.Status200OK)
    .Produces<string>(StatusCodes.Status400BadRequest);

#endregion

#region User

app.MapGet("/getUser/{email}", async (string email, IMediator mediator) =>
{
    var user = await mediator.Send(new GetUserByEmailUseCaseQuery() { Email = email });
    return !user.IsSuccess ? Results.BadRequest(user.Value) : Results.Ok(user.Value);
}).WithName("GetUserByEmail").WithTags("User")
    .Produces<GetUserDto>(StatusCodes.Status200OK)
    .Produces<GetUserDto>(StatusCodes.Status400BadRequest);

app.MapGet("/getUser/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var user = await mediator.Send(new GetUserByIdUseCaseQuery() { Id = id });
    return !user.IsSuccess ? Results.BadRequest(user.Value) : Results.Ok(user.Value);
}).WithName("GetUserById").WithTags("User")
    .Produces<GetUserDto>(StatusCodes.Status200OK)
    .Produces<GetUserDto>(StatusCodes.Status400BadRequest);

app.MapGet("/getUsers", async (IMediator mediator) =>
{
    var user = await mediator.Send(new GetUsersUseCaseQuery());
    return !user.IsSuccess ? Results.BadRequest(user.Value) : Results.Ok(user.Value);
}).WithName("GetUsers").WithTags("User")
    .Produces<List<GetUserDto>>(StatusCodes.Status200OK)
    .Produces<List<GetUserDto>>(StatusCodes.Status400BadRequest);

app.MapGet("/editUser/{id}", async ([FromBody]GetUserDto model, IMediator mediator) =>
{
    var user = await mediator.Send(new EditUserUseCaseCommand()
    {
        Email = model.Email, FirstName = model.FirstName,
        MiddleName = model.MiddleName, LastName = model.LastName
    });
    return !user.IsSuccess ? Results.BadRequest(user) : Results.Ok(user);
}).WithName("EditUser").WithTags("User")
    .Produces<Result>(StatusCodes.Status200OK)
    .Produces<Result>(StatusCodes.Status400BadRequest);

app.MapGet("/deleteUser", async (Guid id, IMediator mediator) =>
{
    var user = await mediator.Send(new DeleteUserUseCaseCommand(){Id = id});
    return !user.IsSuccess ? Results.BadRequest(user.Message) : Results.Ok(user.Message);
}).WithName("DeleteUser").WithTags("User")
    .Produces<Result>(StatusCodes.Status200OK)
    .Produces<Result>(StatusCodes.Status400BadRequest);

#endregion

#region Category

app.MapPost("/category", async([FromBody]ProductCategory category, IMediator mediator) =>
{
    var result = await mediator.Send(new AddCategoryCommand() { Name = category.Name });
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
}).WithName("AddCategory").WithTags("Category")
    .Produces<Result>(StatusCodes.Status200OK)
    .Produces<Result>(StatusCodes.Status400BadRequest);

app.MapGet("/categories", async (IMediator mediator) =>
    {
        var categories = await mediator.Send(new GetCategoryQuery() );
        return categories.IsSuccess ? Results.Ok(categories) :
            !categories.Value.Any() ? Results.NotFound(categories) : Results.BadRequest(categories);
    }).WithName("GetCategories").WithTags("Category")
    .Produces<Result<List<GetCategoryDto>>>(StatusCodes.Status200OK)
    .Produces<Result<List<GetCategoryDto>>>(StatusCodes.Status404NotFound)
    .Produces<Result<List<GetCategoryDto>>>(StatusCodes.Status400BadRequest);

#endregion

#region Product

app.MapPost("/product", async ([FromBody] Havillah.Shared.Product.AddProductDto model, IMediator mediator) =>
    {
        var result = await mediator.Send(new AddProductCommand() { AddProductDto = model });
        return !result.IsSuccess ? Results.BadRequest(result) : Results.Ok(result);
    }).WithName("AddProduct").WithTags("Product")
    .Produces<Result>()
    .Produces<Result>(StatusCodes.Status400BadRequest);

app.MapPost("/updateProduct", async ([FromBody] Havillah.Shared.Product.UpdateProductDto model, IMediator mediator) =>
    {
        var result = await mediator.Send(new UpdateProductProductCommand() { ProductDto = model });
        return result.ResponseCode == "404" ? Results.NotFound(result.Message) :
            result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }).WithName("UpdateProduct").WithTags("Product")
    .Produces<Result>()
    .Produces<Result>(StatusCodes.Status400BadRequest);

app.MapGet("/products", async (IMediator mediator) =>
{
    var products = await mediator.Send(new GetProductsQuery() { });
    return !products.Value.Any() ? Results.NotFound(products.Message) :
              products.IsSuccess ? Results.Ok(products) : Results.BadRequest(products);
}).WithName("GetProducts").WithTags("Product")
    .Produces<Result<List<GetProductDto>>>()
    .Produces<Result<List<GetProductDto>>>(StatusCodes.Status400BadRequest)
    .Produces<Result<List<GetProductDto>>>(StatusCodes.Status404NotFound);

app.MapGet("/product/{id}", async (Guid id, IMediator mediator) =>
{
    var product = await mediator.Send(new GetProductQuery() { Id = id});
    return product?.Value.Id == default ? Results.NotFound(product) : 
              product.IsSuccess ? Results.Ok(product) : Results.BadRequest(product);
}).WithName("GetProductById").WithTags("Product")
    .Produces<Result<GetProductDto>>()
    .Produces<Result<GetProductDto>>(StatusCodes.Status400BadRequest)
    .Produces<Result<GetProductDto>>(StatusCodes.Status404NotFound);

#endregion

#region Expense
app.MapPut("/updateExpense", async ([FromBody] UpdateExpenseDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new UpdateExpenseUseCaseCommand() { ExpenseDto = model });
    return result.ResponseCode == "404" ? Results.NotFound(result.Message) :
    result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
}).WithName("UpdateExpense").WithTags("Expense")
.Produces<Result>(StatusCodes.Status200OK)
.Produces<Result>(StatusCodes.Status404NotFound)
.Produces<Result>(StatusCodes.Status400BadRequest);

app.MapPost("/addExpense", async ([FromBody] AddExpenseDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new AddExpenseCommand() { AddExpenseDto = model });
    return !result.IsSuccess ? Results.BadRequest(result.ResponseCode) :
    Results.Ok(result);
}).WithName("AddExpense").WithTags("Expense")
.Produces<Result>(StatusCodes.Status200OK)
.Produces<Result>(StatusCodes.Status400BadRequest);

app.MapGet("/getExpense/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetExpensesByIdUseCaseQuery() { Id = id });
    return result.Value.Id == default ? Results.NotFound(result.Value)
    : result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Value);
}).WithName("GetExpenseById").WithTags("Expense")
.Produces<Result<GetExpenseDto>>(StatusCodes.Status200OK)
.Produces<Result<GetExpenseDto>>(StatusCodes.Status302Found)
.Produces<Result<GetExpenseDto>>(StatusCodes.Status400BadRequest);

app.MapGet("/getExpense/{date}", async (DateTime ExpenditureDate, IMediator mediator) =>
{
    var result = await mediator.Send(new GetExpensesByDateUseCaseQuery() { ExpenditureDate = ExpenditureDate });
    return result?.Value.ExpenditureDate == null ? Results.NotFound(result) : result.IsSuccess ? Results.Ok(result.Value)
    : Results.BadRequest(result.Value);
}).WithName("GetExpenseByDate").WithTags("Expense")
.Produces<Result<GetExpenseDto>>(StatusCodes.Status200OK)
.Produces<Result<GetExpenseDto>>(StatusCodes.Status400BadRequest)
.Produces<Result<GetExpenseDto>>(StatusCodes.Status404NotFound);

app.MapGet("/getExpenses", async (IMediator mediator) =>
{
    var expenses = await mediator.Send(new GetExpensesUseCaseQuery());
    return !expenses.Value.Any() ? Results.NotFound(expenses.Message) : expenses.IsSuccess ? Results.Ok(expenses) :
    Results.BadRequest(expenses);
}).WithName("GetAllExpenses").WithTags("Expense")
.Produces<List<GetExpenseDto>>(StatusCodes.Status200OK)
.Produces<List<GetExpenseDto>>(StatusCodes.Status404NotFound)
.Produces<List<GetExpenseDto>>(StatusCodes.Status400BadRequest);
#endregion

app.Run();