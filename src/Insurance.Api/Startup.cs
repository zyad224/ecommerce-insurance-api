using System;
using Insurance.Api.Logging;
using Insurance.Api.Services;
using Insurance.Api.Services.Interfaces;
using Insurance.Api.Utilities;
using Insurance.Domain.Interfaces;
using Insurance.Infrastructure.Data;
using Insurance.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
namespace Insurance.Api
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
            services.AddSingleton<ILog, LoggerNLog>();
            services.AddDbContext<DbApiContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));
            services.AddScoped<IDbApiContext>(provider => (IDbApiContext)provider.GetService(typeof(DbApiContext)));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISurchargeRepository, SurchargeRepository>();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
             {
               options.SerializerSettings.ContractResolver = new LowerCaseContractResolver();
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<Insurance.Api.ExceptionHandlerMiddleware.ExceptionHandlerMiddleware>(logger);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseWelcomePage();

        }
    }
}
