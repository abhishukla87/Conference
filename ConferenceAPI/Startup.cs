using ConferenceAPI.Data;
using ConferenceAPI.Helper;
using ConferenceAPI.Middleware;
using ConferenceAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferenceAPI
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
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new HeaderApiVersionReader("Api-version");
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));                  

            services.AddMvc(opt => opt.EnableEndpointRouting = false).
                SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IUserRepository,MockUseRepository>();
            services.AddScoped<ISessionRepository,MockSessionRepository>();
            services.AddScoped<ITopicRepository,MockTopicRepository>();
            services.AddScoped<ISpeakerRepository,MockSpeakerRepository>();

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<VersionCheckMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
