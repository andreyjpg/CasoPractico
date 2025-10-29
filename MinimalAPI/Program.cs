using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Context;
using System;
using System.Security.Cryptography;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register your DbContext with the connection string
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();



app.MapGet("/api/task", async (TaskDbContext db) =>
    await db.Tasks.Where(task => task.Approved != null).OrderBy(task => task.CreatedAt).ToListAsync());

app.MapPost("/login", async ([FromBody] string request, [FromServices] TaskDbContext db) =>
{
try
{
    var data = JsonSerializer.Deserialize<LoginRequest>(request);
    var user = await db.Users
        .Include(u => u.UserRoles)
        .FirstOrDefaultAsync(u => u.Email == data.Email);


    var result = new { Message = "Login correcto", Data = user };
        return user is not null
            ? Results.Ok(result)
            : Results.Unauthorized();
    } catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw ex;
    }
});


app.Run();
