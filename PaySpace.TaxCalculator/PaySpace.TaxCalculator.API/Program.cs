using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaySpace.TaxCalculator.API;
using PaySpace.TaxCalculator.Application;
using PaySpace.TaxCalculator.Infrastructure;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaxDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => {
            sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
        }));
builder.Services.AddApplicationServices();
builder.Services.AddApplicationInfrastructureServices();
builder.Services.AddInfrastructureServices();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.EnsureDatabaseSetup();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
