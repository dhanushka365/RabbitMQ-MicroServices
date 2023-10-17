using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ_Microservices.Infrastructure.IoC;
using RabbitMQ_MicroServices.Transfer.Data.Context;

namespace RabbitMQ_MicroServices.Transfer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TransferDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TransferDBConnection"));
            }
            );

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info { title = "Banking Microservice", version = "v1" });
            });

            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Startup>());

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CS0618 // Type or member is obsolete
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking Microservice V1");
            });

            app.UseRouting(); // Add this line for Endpoint Routing

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This replaces UseMvc for routing
            });
        }
    }
}
