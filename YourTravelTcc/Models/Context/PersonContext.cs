using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YourTravelTcc.Models.Context
{
    public class PersonContext : DbContext
    {
        public PersonContext( DbContextOptions<PersonContext> options ) : base( options )
        {

        }

        //Table name
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Person>().HasKey( m => m.ID );
            base.OnModelCreating( builder );
        }
    }
  
}
