using Ebd.CrossCutting.IoC;
using Ebd.Presentation.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Ebd.Presentation.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            MakeSureLogFolderExists();
            services.ConfigureDependencyInjection(Configuration);
            services.ConfigureRouting();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = false;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ebd.Presentation.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var applicationPath = string.Empty;
                try
                {
                    applicationPath = Configuration.GetValue<string>("AppPath") ?? string.Empty;

                    if (applicationPath != string.Empty)
                        applicationPath = string.Concat("/", applicationPath);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Erro obter AppPath");
                    Console.WriteLine(ex.Message);
                }

                c.SwaggerEndpoint($"{applicationPath}/swagger/v1/swagger.json", "Ebd.Presentation.Api v1");
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

        private void MakeSureLogFolderExists()
        {
            var logFolderPath = Path.Combine(Environment.ContentRootPath, "Logs");

            if (!Directory.Exists(logFolderPath))
                Directory.CreateDirectory(logFolderPath);
        }
    }
}
