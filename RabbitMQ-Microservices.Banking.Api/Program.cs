using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_Microservices.Infrastructure.IoC;
using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Application.Services;
using RabbitMQ_MicroServices.Banking.Data.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddDbContext<BankingDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//------------------------------------------------------------------------------------------------------------------------
//
var services = new ServiceCollection();// Create a service collection
DependencyContainer.RegisterServices(services);// Register your services using the DependencyContainer                        
//var serviceProvider = services.BuildServiceProvider(); // Build the service provider
//----------------------------------------------------------------------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Banking Microservice", Version = "v1" });
});
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(typeof(Program).Assembly, typeof(IAccountService).Assembly);
//builder.Services.AddScoped<IAccountService, AccountService>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AccountService)));
services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

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

