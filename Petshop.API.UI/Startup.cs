using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Petshop.Core.ApplicationService;
using Petshop.Core.ApplicationService.Impl;
using Petshop.Core.DomainService;
using Petshop.Infrastructure.Data;

namespace Petshop.RestAPI.UI
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
            bool devMode = true;
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();

            var provider = services.BuildServiceProvider();

            var ownerRepo = provider.GetService<IOwnerRepository>();
            var petRepo = provider.GetService<IPetRepository>();

            if (devMode)
            {
                var dataInit = new DataInitializer(ownerRepo, petRepo);
                dataInit.InitData();
                //Console.WriteLine(dataInit.InitData()); //I prefer the program telling me that i have injected data.
            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
