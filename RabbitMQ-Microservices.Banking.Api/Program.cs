using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using RabbitMQ_Microservices.Infrastructure.IoC;
using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Data.Context;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddDbContext<BankingDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//------------------------------------------------------------------------------------------------------------------------

var services = new ServiceCollection();// Create a service collection
DependencyContainer.RegisterServices(services);// Register your services using the DependencyContainer

//----------------------------------------------------------------------------------------------------------------------

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

app.UseAuthorization();

app.MapControllers();

app.Run();
