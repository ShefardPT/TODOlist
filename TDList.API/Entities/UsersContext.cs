using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Entities
{
    public class UsersContext : IdentityDbContext<AppUser>
    {
        // Initializing logger
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UsersContext (DbContextOptions<UsersContext> options) : base(options)
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

        // Store for AppUser entities
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
