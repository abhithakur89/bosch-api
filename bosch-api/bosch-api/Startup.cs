using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bosch_api.SignalRHub;
using BoschApi.Entities.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace bosch_api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["SqlServerConnectionString"]), ServiceLifetime.Transient);

            services.AddCors(
                options => options.AddPolicy("AllowCors",
                    builder_ =>
                    {
                        builder_
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    })
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowCors");
            app.UseSignalR(routes =>
            {
                routes.MapHub<BoschApiHub>(Configuration["SignalRHubRelativeUrl"]);
            });

            app.UseMvcWithDefaultRoute();

            //app.UseHttpsRedirection();
            //app.UseMvc();
        }
    }
}
