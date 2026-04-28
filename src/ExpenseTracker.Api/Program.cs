using ExpenseTracker.Api.Endpoints;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Services;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<ExpenseTrackerDbContext>());

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AngularPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTransactionEndpoints();

app.Run();