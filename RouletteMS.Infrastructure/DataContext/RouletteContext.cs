using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouletteMS.Infrastructure.Entities;

namespace RouletteMS.Infrastructure.DataContext
{
    public class RouletteContext : DbContext
    {
        public DbSet<Roulette> Roulettes { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<User> Users { get; set; }
        public RouletteContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RouletteApp")
                        .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
