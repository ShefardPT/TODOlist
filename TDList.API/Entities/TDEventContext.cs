using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Entities
{
    public class TDEventContext : DbContext
    {
        DbSet<TDEvent> TDEvents { get; set; }
    }
}
    