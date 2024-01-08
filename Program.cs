using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;
using MyApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<MyApiContext>();
builder.Services.AddDbContext<MyApiDbContext>();
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    // Using the ./ApiResponse.cs wrapper for the response to get a proper response from all endpoints.
    return ApiResponse<IEnumerable<WeatherForecast>>.SuccessResponse(forecast);
})
.WithName("GetWeatherForecast")
.WithOpenApi();


// Users
app.MapPost("/CreateUsers", async (User user, UsersService usersService) =>
{
    var createdUser = await usersService.CreateUser(user);
    if (createdUser == null)
    {
        return ApiResponse<User>.ErrorResponse("An error occurred while creating the user.");
    }
    return ApiResponse<User>.SuccessResponse(createdUser, "User created successfully.");
})
.WithName("CreateUser");


app.MapGet("GetUsers/{id}", async (int id, UsersService usersService) =>
{
    var user = await usersService.GetUserById(id);
    if (user == null)
    {
        return ApiResponse<User>.ErrorResponse($"User with id {id} not found");
    }
    return ApiResponse<User>.SuccessResponse((User)user);
})
.WithName("GetUser");


app.MapGet("GetAllUsers", async (UsersService usersService) =>
{
    var users = await usersService.GetAllUsers();
    return ApiResponse<IEnumerable<User>>.SuccessResponse(users);
})
.WithName("GetAllUsers");


app.MapPut("/users/{id}", async (int id, User user, UsersService usersService) =>
{
    var updatedUser = await usersService.UpdateUser(id, user);
    // if (updatedUser == null)
    // {
    //     return ApiResponse<User>.ErrorResponse($"User with id {id} not found");
    // }
    return ApiResponse<User>.SuccessResponse(updatedUser, "User updated successfully.");
})
.WithName("UpdateUser");


app.MapDelete("/users/{id}", async (int id, UsersService usersService) =>
{
    var result = await usersService.DeleteUser(id);
    if (result)
    {
        return ApiResponse<string>.SuccessResponse($"User with id {id} deleted");
    }
    else
    {
        return ApiResponse<string>.ErrorResponse($"User with id {id} not found");
    }
})
.WithName("DeleteUser");

// Auth
app.MapPost("/Auth/register", (UserRegistration registration) => { /* Register a new user */ })
   .WithName("Register");

app.MapPost("/Auth/login", (UserLogin login) => { /* Log in a user */ })
   .WithName("Login");

// File Upload challenge 
app.MapPost("/upload", (IFormFile file) => { /* Handle file upload */ })
   .WithName("UploadFile");

// Pagination and Filtering
app.MapGet("/products", (int page, int pageSize, string filter) => { /* Get products with pagination and filtering */ })
   .WithName("GetProducts");

// Error Handling
app.MapGet("/products/{id}", (int id) =>
{
    /* Get a product by id, return 404 if not found */
})
   .WithName("GetProduct");

app.Run();

internal class MyApiContext
{
}
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
