using ControleEstoque.Repository;
using ControleEstoque.Service;
using ControleEstoque.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ControleEstoque
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RepositoryContext>();

            services.AddTransient<IRepositoryCRUD, RepositoryCRUD>();

            services.AddTransient<IRepositoryService, RepositoryService>();

            services.AddTransient<IRepositoryChanges, RepositoryChanges>();

            services.AddTransient<IServiceBusController, ServiceBusController>();

            services.AddTransient<IMessagePublisher, MessagePublisher>();

            services.AddTransient<MessageConsume>();

            services.AddHostedService<MessageConsume>();

            services.AddTransient<ITopicClient>(c =>
                new TopicClient(
                    Configuration.GetValue<string>("ServiceBus:ConnectionString"),
                    Configuration.GetValue<string>("ServiceBus:EntityPath")));

            services.AddTransient<ISubscriptionClient>(c =>
                new SubscriptionClient(
                    Configuration.GetValue<string>("ServiceBus:ConnectionString"),
                    Configuration.GetValue<string>("ServiceBus:EntityPath"),
                    Configuration.GetValue<string>("ServiceBus:Subscription")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.1",
                    Title = "eVendas API - Aceleração Global Dev Avanade",
                    Description = "A simple example of ASP.NET Core web API using Azure Service Bus",
                    Contact = new OpenApiContact
                    {
                        Name = "Flávio Santos",
                        Email = "flavio.gds@gmail.com",
                        Url = new Uri("https://github.com/flaviogds")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/flaviogds"),
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _1)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controle de Estoque");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
