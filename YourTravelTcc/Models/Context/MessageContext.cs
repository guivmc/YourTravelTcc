using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YourTravelTcc.Models.Context
{
    public class MessageContext : DbContext
    {
        //Table name
        public DbSet<Guide> Guide { get; set; }
        public DbSet<Traveler> Traveler { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Message> Message { get; set; }

        public MessageContext( DbContextOptions<MessageContext> options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<Message>().HasKey( m => m.ID );
            base.OnModelCreating( builder );
        }
    }
}
