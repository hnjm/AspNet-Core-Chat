using Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .Build();

            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Database
            services.AddDbContext<Context>(opt =>
                opt.UseSqlServer(
                    Configuration.GetConnectionString("Default")));

            // Garante que eu tenha um contexto por requisição
            services.AddScoped<Context, Context>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
