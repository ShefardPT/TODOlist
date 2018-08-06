﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Entities
{
    public class TDEventContext : DbContext
    {
        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public TDEventContext(DbContextOptions<TDEventContext> options) : base(options)
        {
            _logger.Debug("Database is going to migrate");
            try
            {
                Database.Migrate();
                _logger.Debug("Database was migrated successfully");
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while database migration", ex);
            }

        }

        public DbSet<TDEvent> TDEvents { get; set; }
    }
}
    