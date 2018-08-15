using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Startup.Configuration["connectionString:DefaultConnection"];

            services.AddDbContext<Entities.AppUserContext>(o => o.UseSqlServer(connectionString));
            services.AddIdentity<Entities.AppUser, IdentityRole>().AddEntityFrameworkStores<Entities.AppUserContext>();

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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();

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
