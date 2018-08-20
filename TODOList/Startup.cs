using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace TODOList
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Cfg/AppSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Cfg/AppSettings/{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Startup.Configuration["connectionString:DefaultConnection"];

            services.AddDbContext<Entities.AppUserContext>(o => o.UseSqlServer(connectionString));
            services.AddIdentity<Entities.AppUser, IdentityRole>(o => {
                o.Password.RequiredLength = 8;
                o.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<Entities.AppUserContext>();

            services.AddDbContext<Entities.TDEventContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<Services.ITDEventRepository, Services.TDEventRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                                Entities.TDEventContext tdEventContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.TDEvent, Models.TDEventDTO>();
                cfg.CreateMap<Entities.TDEvent, Models.TDEventToManip>();
                cfg.CreateMap<Models.TDEventDTO, Entities.TDEvent>();
                cfg.CreateMap<Models.TDEventToManip, Entities.TDEvent>();
            });
        }
    }
}
