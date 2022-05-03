using AJ60J7_HFT_2021222.Logic;
using AJ60J7_HFT_2021222.Models;
using AJ60J7_HFT_2021222.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<IEngineLogic, EngineLogic>();

            services.AddTransient<Car, Car>();
            services.AddTransient<Brand, Brand>();
            services.AddTransient<Engine, Engine>();

            services.AddTransient<IDefaultRepository<Car>, CarRepository>();
            services.AddTransient<IDefaultRepository<Brand>, BrandRepository>();
            services.AddTransient<IDefaultRepository<Engine>, EngineRepository>();

            //services.AddTransient<ICarRepository, CarRepository>();
            //services.AddTransient<IBrandRepository, BrandRepository>();
            //services.AddTransient<IEngineRepository, EngineRepository>();

            services.AddTransient<CarShopContext, CarShopContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
