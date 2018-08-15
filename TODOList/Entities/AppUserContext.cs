using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.Entities
{
    public class AppUserContext :IdentityDbContext<AppUser>
    {
        // Initializing logger
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public AppUserContext(DbContextOptions<AppUserContext> options) : base(options)
        {
            _logger.Debug("Database is going to migrate");
            try
            {
                Database.Migrate();
                _logger.Debug("Database was migrated successfully");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Thrown exception while database migration");
            }

        }

        // Store for TDEvent entities
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
