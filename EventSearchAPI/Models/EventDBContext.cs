using EventClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSearchAPI.Models
{
    public class EventDBContext : DbContext
    {
        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
            
        }

        public DbSet<DOEvent> EventItems { get; set; }
    }
}
