using FluentAssertions.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
//builder.Services.AddMediatR(typeof(Program).Assembly);
//var services = new ServiceCollection();// Create a service collection
//DependencyContainer.RegisterServices(services);// Register your services using the DependencyContainer
                                              
//var serviceProvider = services.BuildServiceProvider(); // Build the service provider
//----------------------------------------------------------------------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Banking Microservice", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking Microservice v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

