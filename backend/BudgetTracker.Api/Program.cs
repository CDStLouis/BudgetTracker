using BudgetTracker.Api.Data;
using BudgetTracker.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var configuredOrigins = builder.Configuration["AllowedOrigins"] ?? "http://localhost:5173";
        var allowedOrigins = configuredOrigins
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(origin => origin.TrimEnd('/'))
            .ToArray();

        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();              
    });
});

builder.Services.AddDbContext<BudgetTrackerContext>(options =>
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("BudgetTrackerTests");
        return;
    }

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        )
    );
});

builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseResponseCaching();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "An unexpected error occurred"
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<BudgetTrackerContext>();
        DatabaseSeeder.Seed(context);
    }
    catch (Exception ex)
    {
        // Don't let a seeding failure crash the whole app on startup;
        // the API should still come up so /api/transactions can serve whatever is in the DB.
        logger.LogError(ex, "Database seeding failed during startup.");
    }
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }
