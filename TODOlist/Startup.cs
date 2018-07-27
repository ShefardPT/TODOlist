using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TODOlist.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace TODOlist.API
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)

                // Intergrating config files
                .AddJsonFile("Cfg/Cfg.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Cfg/Cfg.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // SQL server sets
            var connetionString = @"Server = (localdb)\MSSQLLocalDB; DataBase = EventInfoDB; Trusted_Connection = true;";
            services.AddDbContext<TDEventContext>(o => o.UseSqlServer(connetionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
        }
    }
}
