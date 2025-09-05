using UserManagementApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use custom logging middleware
app.UseMiddleware<LoggingMiddleware>();

// === In-memory user list ===
var users = new List<User>();
var idCounter = 1;

// === CRUD Endpoints ===

// GET all users
app.MapGet("/users", () =>
    Results.Ok(users)
);

// GET user by ID
app.MapGet("/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user is not null
        ? Results.Ok(user)
        : Results.NotFound(new { message = "User not found" });
});

// POST create new user
app.MapPost("/users", ([FromBody] UserDto newUser) =>
{
    if (!MiniValidator.TryValidate(newUser, out var errors))
        return Results.BadRequest(errors);

    var user = new User { Id = idCounter++, Name = newUser.Name, Email = newUser.Email, Phone = newUser.Phone };
    users.Add(user);
    return Results.Created($"/users/{user.Id}", user);
});

// PUT update user
app.MapPut("/users/{id}", (int id, [FromBody] UserDto updatedUser) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound(new { message = "User not found" });

    if (!MiniValidator.TryValidate(updatedUser, out var errors))
        return Results.BadRequest(errors);

    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;
    user.Phone = updatedUser.Phone;
    return Results.Ok(user);
});

// DELETE user
app.MapDelete("/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound(new { message = "User not found" });
    users.Remove(user);
    return Results.Ok(user);
});
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();

// === Models ===
public record User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = ""; // <-- Added Phone property
}

public record UserDto
{
    [Required]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string Phone { get; set; } = ""; // <-- Added Phone property
}

// === Minimal validation helper ===
public static class MiniValidator
{
    public static bool TryValidate<T>(T obj, out Dictionary<string, string[]> errors)
    {
        var context = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(obj, context, validationResults, true);

        errors = validationResults
            .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
            .ToDictionary(
                g => g.Key,
                g => g.Select(r => r.ErrorMessage ?? "").ToArray()
            );

        return isValid;
    }
}