using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABSA.PB.Data;
using Microsoft.EntityFrameworkCore;
using ABSA.PB.Core.Interfaces;
using ABSA.PB.Core.Services;
using ABSA.PB.Data.Interfaces;
using ABSA.PB.Data.Services;
using System.Text.Json.Serialization;

namespace ABSA.PB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment _env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ABSA.Phonebook.API", Version = "v1" });
            });
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@$"{_env.ContentRootPath}\Log\Phonebook.text", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            services.AddSingleton<ILogger>(Log.Logger);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IPhonebookService, PhonebookService>();
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<PhonebookDBContext>(c => c.UseSqlServer(Configuration.GetConnectionString("PhonebookConnection")));
            services.AddCors(c =>
            {
                c.AddPolicy("APB", p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ABSA.Phonebook.API v1"));
            }

            app.UseCors("APB");

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