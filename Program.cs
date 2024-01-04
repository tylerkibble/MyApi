using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;

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
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


// Users
app.MapPost("/CreateUsers", async (User user, UsersService usersService) =>
{
    /* Create a new user */

    // if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
    // {
    //     return Task.FromResult(Results.BadRequest("Error"));
    // }

    var createdUser = await usersService.CreateUser(user);
    Console.WriteLine(createdUser);
    if (createdUser == null)
    {
        return Results.Problem("An error occurred while creating the user.", statusCode: 500);
    }
    Console.WriteLine(createdUser.Name, createdUser.Email);
    return Results.Created($"/users/{createdUser.Id}", createdUser);
})
.WithName("CreateUser");

app.MapGet("GetUsers/{id}", async (int id, UsersService usersService) => 
{
    var user = await usersService.GetUserById(id);
    if(user == null)
    {
        return Results.NotFound($"User with id {id} not found");
    }


    return Results.Ok(user);
})
.WithName("GetUser");
// app.MapGet("GetUsers/{id}", (int id) => {/* Get a user by id */})
// .WithName("GetUser");

app.MapGet("GetAllUsers", async (UsersService usersService) =>
{
     var users = await usersService.GetAllUsers();
    foreach (var user in users)
    {
        Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
    }

    return users;
})
.WithName("GetAllUsers");

app.MapPut("/users/{id}", (int id, User user) => { /* Update a user */ })
.WithName("UpdateUser");

app.MapDelete("/users/{id}", (int id) => { /* Delete a user */ })
.WithName("DeleteUser");

// Auth
app.MapPost("/register", (UserRegistration registration) => { /* Register a new user */ })
   .WithName("Register");

app.MapPost("/login", (UserLogin login) => { /* Log in a user */ })
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
