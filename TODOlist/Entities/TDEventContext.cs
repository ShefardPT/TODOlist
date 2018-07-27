using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOlist.API.Entities
{
    public class TDEventContext : DbContext
    {
        public TDEventContext(DbContextOptions<TDEventContext> options): base(options)
        {
            Database.Migrate();
        }

        public DbSet<TDEvent> TDEvents { get; set; }
    }
}
