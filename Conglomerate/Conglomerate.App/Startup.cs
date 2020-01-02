using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using AutoMapper;

using Conglomerate.Cqrs.Queries;
using Conglomerate.Data.Contexts;
using Conglomerate.Process.Common.Constants;
using Conglomerate.Process.Common.Jobs;
using Conglomerate.ServiceRepository.Repositories;
using Conglomerate.ServiceRepository.Services;

using Hangfire;
using Hangfire.MySql.Core;

using Lamar;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Conglomerate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureContainer(ServiceRegistry services)
        {
            services.AddControllers();

            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });

            var assemblies =
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Where(s => s.GetName().Name.StartsWith(nameof(Conglomerate)))
                    .ToArray();

            // Auto Mapper
            services.AddAutoMapper(assemblies);

            // Services
            services.For<IIngredientService>().Use<IngredientService>();

            // Repositories
            services.For<IIngredientRepository>().Use<IngredientRepository>();

            // Mediatr
            services.AddMediatR(assemblies);

            services.Scan(scanner =>
            {
                foreach (var assembly in assemblies)
                {
                    scanner.Assembly(assembly);
                }

                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
            });

            // For some reason we need to register one of the handlers for Lamar to register all of them
            services.For<IRequestHandler<IngredientGetAll.Query, IList<IngredientGetAll.Ingredient>>>().Use<IngredientGetAll.Handler>();

            services.For<IMediator>().Use<Mediator>().Transient();
            services.For<ServiceFactory>().Use(ctx => ctx.GetInstance);

            // Db Contexts
            services.AddDbContext<SandwichShopContext>(ServiceLifetime.Transient);

            // Hangfire
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(Configuration.GetConnectionString("Hangfire"), new MySqlStorageOptions()
                {
                    TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                    QueuePollInterval = TimeSpan.FromSeconds(2),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 50000,
                    TransactionTimeout = TimeSpan.FromMinutes(1),
                    TablePrefix = "Hangfire"
                })));

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { JobQueues.DEFAULT, JobQueues.API };
            });

            services.For<IJobFactory>().Use<JobFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
