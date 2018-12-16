using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCollectAPI.Models
{
    public class IGAWContext : DbContext
    {
        public IGAWContext(DbContextOptions<IGAWContext> options)
            : base(options)
        {

        }

        public DbSet<IGAWEvent> IGAWEvents { get; set; }
        public DbSet<IGAWOrder> IGAWOrders { get; set; }
    }
}
