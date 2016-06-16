using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTC.Models
{
    public class MyTCContext:DbContext
    {
        public MyTCContext(DbContextOptions<MyTCContext> options)
            : base(options)
        { }
        public DbSet<Attractions> Attractions { get; set; }
        public DbSet<HotButtons> HotButtons { get; set; }
        public DbSet<LocaleGenre> LocaleGenre { get; set; }
        public DbSet<Travelers> Travelers { get; set; }
        public DbSet<VisitedAttractions> VisitedAttractions { get; set; }
    }
}
