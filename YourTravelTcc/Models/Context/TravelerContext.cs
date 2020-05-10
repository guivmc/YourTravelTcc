using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YourTravelTcc.Models.Context
{
    public class TravelerContext : DbContext 
    {
        public TravelerContext( DbContextOptions<TravelerContext> options ) : base( options )
        {

        }

        //Table name
        public DbSet<Traveler> Traveler { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Traveler>().HasKey( m => m.ID );
            base.OnModelCreating( builder );
        }
    }
}
