using Api.Database;
using Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.IgnoreNullValues = true;
                });


            #region Database
            services.AddDbContext<Context>(opt =>
                opt.UseSqlServer(
                    Configuration.GetConnectionString("Default")));

            // Garante que eu tenha um contexto por requisição
            services.AddTransient<Context, Context>();
            #endregion

            #region Swagger

            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });

            });

            #endregion

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API");
            });

            app.UseRouting();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<ChatHub>("/chat");
                });
        }
    }
}
