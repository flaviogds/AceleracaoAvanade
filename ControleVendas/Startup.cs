using ControleVendas.Repository;
using ControleVendas.Service;
using ControleVendas.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ControleVendas
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

            services.AddScoped<IRepositoryCRUD, RepositoryCRUD>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddSingleton<IServiceBusController, ServiceBusController>();

            services.AddSingleton<IMessagePublisher, MessagePublisher>();

            services.AddSingleton<MessageConsume>();

            services.AddHostedService<MessageConsume>();

            services.AddSingleton<ITopicClient>(c =>
                new TopicClient(
                    Configuration.GetValue<string>("ServiceBus:ConnectionString"),
                    Configuration.GetValue<string>("ServiceBus:EntityPath")));

            services.AddSingleton<ISubscriptionClient>(c =>
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
                        Url = new Uri("https://github.com/flaviogds"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License MIT",
                        Url = new Uri("https://github.com/flaviogds/AceleracaoAvanade/blob/master/LICENSE.md"),
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controle de Vendas");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
