using System;
using System.Linq;

using AutoMapper;

using Conglomerate.Data.Contexts;
using Conglomerate.ServiceRepository.Repositories;
using Conglomerate.ServiceRepository.Services;

using Lamar;

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

            // Auto Mapper
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(s => s.GetName().Name.StartsWith(nameof(Conglomerate)));
            services.AddAutoMapper(assemblies);

            // Services
            services.For<IIngredientService>().Use<IngredientService>();

            // Repositories
            services.For<IIngredientRepository>().Use<IngredientRepository>();

            // Db Contexts
            services.AddDbContext<SandwichShopContext>(ServiceLifetime.Transient);
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
