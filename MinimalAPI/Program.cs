using Microsoft.EntityFrameworkCore;
using MinimalAPI.Context;
using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register your DbContext with the connection string
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();



app.MapGet("/api/task", async (TaskDbContext db) =>
    await db.Task.ToListAsync());

//app.MapPost("/login", async ([FromBody] LoginRequest request, [FromServices] TaskDbContext db) =>
//{
//    using var sha256 = SHA256.Create();
//    var inputBytes = Encoding.UTF8.GetBytes(request.Password);
//    var hashBytes = sha256.ComputeHash(inputBytes);

//    var user = await db.User
//        .Include(u => u.UserRoles)
//        .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password.SequenceEqual(hashBytes));

//    return user is not null
//        ? Results.Ok(new { Message = "Login correcto", user.Email })
//        : Results.Unauthorized();
//});


app.Run();
