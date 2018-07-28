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

namespace TDList.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Cfg/AppSettings.json",                            optional: false,    reloadOnChange: true)
                .AddJsonFile($"Cfg/AppSettings/{env.EnvironmentName}.json",     optional: true,     reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddScoped<Services.ITDEventRepository, Services.TDEventRepository>();

            var connectionString = Startup.Configuration["connectionString:TDListDBConnectionString"];
            services.AddDbContext<Entities.TDEventContext>(o => o.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Entities.TDEventContext tdEventContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseMvc();

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            tdEventContext.EnsureSeedDataForContext();

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
